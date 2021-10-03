using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Product_search.xaml
    /// </summary>
    public partial class Product_search : Window
    {
        public Product_search()
        {
            InitializeComponent();
        }

        public void Click_add_product_plus(object sender, RoutedEventArgs e)
        {

        }

        public void Click_back(object sender, RoutedEventArgs e)
        {
            if (MainWindow.IsSearchProducts == true)
            {
                After_logging_in window2 = new After_logging_in();
                window2.Show();
                Close();
            }
            else if (MainWindow.IsSearchProducts == false)
            {
                MainWindow.IsSearchProducts = true;
                Sales1 window3 = new Sales1();
                window3.Show();
                Close();
            }
        }

        public void Click_to_find_a_product(object sender, RoutedEventArgs e)
        {

        }
    }
}
