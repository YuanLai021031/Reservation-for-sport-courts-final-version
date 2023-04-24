using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using MySql.Data.MySqlClient;
using System.Data;
using Reservation_for_sport_courts_final_version.classes;
using Reservation_for_sport_courts_final_version.service;
using System.Windows.Markup;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Xml.Linq;
using MySqlX.XDevAPI.Relational;

namespace Reservation_for_sport_courts_final_version
{
    public partial class Booking : Window
    {
        private int userId;
        private MySqlConnector sql;
        private ReservationCollection reservationCollection;
        private DateTime date;
        private string selectedItem;
        private int totalPrice = 0;
        
        public Booking()
        {
            InitializeComponent();
            sql = App.sql;
            reservationCollection = new ReservationCollection();
            reservationCollection.init(8, 21);
        }

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {   
            foreach(var courtName in reservationCollection.reservations)
                {
                    foreach(var slotDetail in courtName.Value)
                    {
                    if (slotDetail.isSelected)
                    {
                        var courtType = sql.executeQuery(
                            "Select * from court_type where type = @type",
                            new List<string>() { "id", "price_per_hour" },
                            new Dictionary<string, object> { { "@type", selectedItem } }
                        )[0];
                        var court = sql.executeQuery(
                            "Select * from courts where court_type_id = @court_type_id",
                            new List<string>() { "id", "name" },
                            new Dictionary<string, object> { { "@court_type_id", courtType["id"] } }
                        )[0];
                        var courtId = court["id"];

                        sql.executeNonQuery(
                            "Insert into court_reservation " +
                            "(user_id, court_id, date, from_hour, to_hour, price) " +
                            "values (@user_id, @court_id, @date, @from_hour, @to_hour, @price)",
                            new Dictionary<string, object>()
                            {
                                { "@user_id", userId },
                                { "@court_id", courtId },
                                { "@date", date },
                                { "@from_hour", slotDetail.hour },
                                { "@to_hour", slotDetail.hour + 1},
                                { "@price", slotDetail.price }
                            });
                        Console.WriteLine($"{courtId}: {date.Date} : {slotDetail.hour} : {slotDetail.price}");
                        }
                }
            }
        }

        private void onLookupParameterChange(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxItem = SportComboBox.SelectedItem as ComboBoxItem;
            selectedItem = comboBoxItem.Content.ToString().ToLower();
            if (BookingDatePicker.SelectedDate == null)
            {
                return;
            }
            date = BookingDatePicker.SelectedDate.Value.Date;
            readData();
            drawBookingTable();
        }

        private void readData()
        {
            reservationCollection.init(8, 21);
            var courtType = sql.executeQuery(
                "Select * from court_type where type = @type",
                new List<string>() { "id", "price_per_hour" },
                new Dictionary<string, object> { { "@type", selectedItem } }
            )[0];
            var courts = sql.executeQuery(
                "Select * from courts where court_type_id = @court_type_id",
                new List<string>() { "id", "name" },
                new Dictionary<string, object> { { "@court_type_id", courtType["id"] } }
                );
            //TODO
            var random = new Random();
            var reservation = reservationCollection.reservations;
            foreach (var court in courts)
            {
                var name = court["name"];
                var id = court["id"];
                var hoursAndDetail = new List<SlotDetail>();
                reservation.Add(name, hoursAndDetail);
                for(int hour = 8; hour <= 21; hour++)
                {
                    var slotCell = new SlotDetail
                    {
                        courtId = id,
                        hour = hour,
                        isSelected = false,
                        price = $"{random.Next(10, 20)}",
                        isReserved = random.Next(0, 100) > 50 ? true : false
                    };
                    if (slotCell.isReserved) slotCell.price = "";
                    hoursAndDetail.Add(slotCell);
                }
            }
        }

        private void drawBookingTable()
        {
            BookingTable.Columns.Clear();
            BookingTable.Visibility = Visibility.Visible;

            var items = new List<BookingTableRow>();
            var reservations = reservationCollection.reservations;

            var hourColumn = new DataGridTextColumn();
            hourColumn.Header = "";
            hourColumn.Binding = new Binding("hour");
            BookingTable.Columns.Add(hourColumn);


            for (int hour = 8; hour < 21; hour++)
            {
                var row = new BookingTableRow();
                row.hour = hour.ToString("D2") + ":00-" + (hour + 1).ToString("D2") + ":00";
                foreach (var reservation in reservations)
                {
                    var court = reservation.Key;
                    row.details.Add(reservation.Key, reservations[court][hour - 8]);
                }
                items.Add(row);
            }

            foreach (var reservation in reservations)
            {
                var court = reservation.Key;
               /* var column = new DataGridTextColumn();
                column.Binding = new Binding(string.Format("details[{0}].price", court));
*/
                /*var cellStyle = new Style(typeof(DataGridCell));
                cellStyle.Setters.Add(new EventSetter(DataGridCell.LoadedEvent, new RoutedEventHandler(OnCellLoaded)))*/;

                var column = new DataGridTemplateColumn();
                column.Header = court;
                var factory = new FrameworkElementFactory(typeof(TextBlock));
                factory.SetBinding(TextBlock.DataContextProperty, new Binding(string.Format("details[{0}]", court)));
                factory.SetBinding(TextBlock.TextProperty, new Binding(string.Format("price", court)));
                factory.AddHandler(
                    DataGridCell.MouseLeftButtonDownEvent,
                    new MouseButtonEventHandler((sender, e) =>
                    {
                        var baseObject = sender as FrameworkElement;
                        var cell = baseObject.DataContext as SlotDetail;
                        var tb = baseObject as TextBlock;
                        
                        if (cell.isReserved)
                        {
                            MessageBox.Show("This slot is already reserved");
                            return;
                        }
                        cell.isSelected = !cell.isSelected;
                        if (cell.isSelected)
                        {
                            totalPrice += int.Parse(cell.price);
                            tb.Background = Brushes.LightBlue;
                        } else
                        {
                            totalPrice -= int.Parse(cell.price);
                            tb.Background = Brushes.Transparent;
                        }
                        TotalRriceTextBlock.Text = totalPrice == 0 ? "" : totalPrice.ToString();
                        TotalRriceTextBlock.FontSize = 24; 
                        TotalRriceTextBlock.Foreground = Brushes.White; 
                        TotalRriceTextBlock.FontWeight = FontWeights.Bold; 
                        TotalRriceTextBlock.TextAlignment = TextAlignment.Center;
                    }));

                column.CellStyle = new Style(typeof(DataGridCell));
                DataTrigger isReservedTrigger = new DataTrigger()
                {
                    Binding = new Binding(string.Format("details[{0}].isReserved", court)),
                    Value = true
                };
                isReservedTrigger.Setters.Add(new Setter(
                    DataGridCell.BackgroundProperty,
                    Brushes.Red
                ));
                column.CellStyle.Triggers.Add(isReservedTrigger);
                column.CellTemplate = new DataTemplate() { VisualTree = factory };
                BookingTable.Columns.Add(column);
            }
            BookingTable.ItemsSource = items;           
        }
    }
    }
