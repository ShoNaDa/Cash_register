using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Refund_of_products.xaml
    /// </summary>
    public partial class Refund_of_products : Window
    {
        public Refund_of_products()
        {
            InitializeComponent();
        }

        public void Click_back(object sender, RoutedEventArgs e)
        {
            if (MainWindow.IsCashier == true)
            {
                After_login_in_cashier window9 = new After_login_in_cashier();
                window9.Show();
                Close();
            }
            else if (MainWindow.IsCashier == false)
            {
                After_logging_in window2 = new After_logging_in();
                window2.Show();
                Close();
            }
        }

        public void Click_to_find_a_cheque(object sender, RoutedEventArgs e)
        {
            Button_find_a_cheque.Visibility = Visibility.Hidden;
            Search_cheque.Visibility = Visibility.Visible;
        }
    }
}
