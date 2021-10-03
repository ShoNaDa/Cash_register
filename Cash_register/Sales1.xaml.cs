using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Sales1.xaml
    /// </summary>
    public partial class Sales1 : Window
    {
        public Sales1()
        {
            InitializeComponent();
        }

        public void Click_to_be_paid(object sender, RoutedEventArgs e)
        {

        }

        public void Click_add_product(object sender, RoutedEventArgs e)
        {
            MainWindow.IsSearchProducts = false;
            Product_search window4 = new Product_search();
            window4.Show();
            Close();
        }

        public void Click_back(object sender, RoutedEventArgs e)
        {
            if(MainWindow.IsCashier == true)
            {
                After_login_in_cashier window9 = new After_login_in_cashier();
                window9.Show();
                Close();
            }
            else if(MainWindow.IsCashier == false)
            {
                After_logging_in window2 = new After_logging_in();
                window2.Show();
                Close();
            }
        }
    }
}
