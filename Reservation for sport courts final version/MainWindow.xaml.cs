using MySql.Data.MySqlClient;
using Reservation_for_sport_courts_final_version;
using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace WPFLoginRegisterDemo
{
    public partial class MainWindow : Window
    {
        private string connectionString = "server=mariadb.vamk.fi;port=3306;database=e2000581_court_reservation;uid=e2000581;password=aa69aRMFWmT;";


        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            ChangeBackgroundColor(Colors.LightBlue);
            string username = txtLoginUsername.Text;
            string password = txtLoginPassword.Password;

            // Perform authentication logic here
            bool isAuthenticated = Authenticate(username, password);

            if (isAuthenticated)
            {
                this.Hide();
                var booking = new Booking();
                booking.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            ChangeBackgroundColor(Colors.LightBlue);
            string username = txtRegisterUsername.Text;
            string password = txtRegisterPassword.Password;
            string confirmPassword = txtRegisterConfirmPassword.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (!password.Equals(confirmPassword))
            {
                MessageBox.Show("Password and Confirm Password do not match. Please try again.");
                return;
            }

            // Perform registration logic here
            bool isRegistered = Register(username, password);

            if (isRegistered)
            {
                MessageBox.Show("Registration successful!");
                txtRegisterUsername.Clear();
                txtRegisterPassword.Clear();
                txtRegisterConfirmPassword.Clear();
            }
            else
            {
                MessageBox.Show("Registration failed. Please try again.");
            }
        }

        private void ChangeBackgroundColor(Color color)
        {
            var brush = new SolidColorBrush(color);
            brush.Freeze();
            this.Dispatcher.Invoke(() => {
                this.Background = brush;
            });
        }

        private bool Authenticate(string username, string password)
        {
            bool isAuthenticated = false;

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM users WHERE username=@username AND password=@password";
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                isAuthenticated = true;
            }

            return isAuthenticated;
        }
        private bool Register(string username, string password)
        {
            bool isRegistered = false;

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO users (username, password) VALUES (@username, @password)";
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            int result = command.ExecuteNonQuery();
            if (result > 0)
            {
                isRegistered = true;
            }
            return isRegistered;
        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
