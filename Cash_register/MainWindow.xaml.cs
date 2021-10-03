using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool IsSearchProducts = true;
        public static bool IsCashier;
        public static bool IsAdmin;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Click_to_cashier(object sender, RoutedEventArgs e)
        {
            IsAdmin = false;
            Authorization authorization = new Authorization();
            authorization.Show();
            Close();
        }

        public void Click_to_admin(object sender, RoutedEventArgs e)
        {
            IsAdmin = true;
            Authorization authorization = new Authorization();
            authorization.Show();
            Close();
        }
    }
}
