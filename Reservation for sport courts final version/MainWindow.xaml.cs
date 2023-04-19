using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFLoginRegisterDemo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            ChangeBackgroundColor(Colors.LightBlue);
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            ChangeBackgroundColor(Colors.LightBlue);
        }

        private void ChangeBackgroundColor(Color color)
        {
            var brush = new SolidColorBrush(color);
            brush.Freeze();
            this.Dispatcher.Invoke(() => {
                this.Background = brush;
            });
        }

        private void btnLogin_Click()
        {

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
