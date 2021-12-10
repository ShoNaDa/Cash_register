using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для After_logging_in.xaml
    /// </summary>
    public partial class After_logging_in : Window
    {
        public After_logging_in()
        {
            InitializeComponent();
        }

        public void Click_to_sales(object sender, RoutedEventArgs e)
        {
            MainWindow.isCashier = false;

            Sales1 window3 = new Sales1();
            window3.Show();
            Close();
        }

        public void Click_to_refund(object sender, RoutedEventArgs e)
        {
            MainWindow.isCashier = false;

            Refund_of_products window5 = new Refund_of_products();
            window5.Show();
            Close();
        }

        public void Click_to_products(object sender, RoutedEventArgs e)
        {
            Product_search window4 = new Product_search();
            window4.Show();
            Close();
        }

        public void Click_to_statement(object sender, RoutedEventArgs e)
        {
            MainWindow.isCashier = false;

            Statements window6 = new Statements();
            window6.Show();
            Close();
        }

        public void Click_to_settings(object sender, RoutedEventArgs e)
        {
            Settings window7 = new Settings();
            window7.Show();
            Close();
        }

        public void Click_to_calculator(object sender, RoutedEventArgs e)
        {
            MainWindow.isCashier = false;

            Calculator window8 = new Calculator();
            window8.Show();
            Close();
        }
    }
}
