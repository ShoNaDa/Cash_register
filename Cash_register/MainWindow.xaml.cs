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
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Click_to_cashier(object sender, RoutedEventArgs e)
        {
            After_login_in_cashier window9 = new After_login_in_cashier();
            window9.Show();
            Close();
        }

        public void Click_to_admin(object sender, RoutedEventArgs e)
        {
            After_logging_in window2 = new After_logging_in();
            window2.Show();
            Close();
        }
    }
}
