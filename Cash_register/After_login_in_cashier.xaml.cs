using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для After_login_in_cashier.xaml
    /// </summary>
    public partial class After_login_in_cashier : Window
    {
        public After_login_in_cashier()
        {
            InitializeComponent();
        }

        public void Click_to_sales_cashier(object sender, RoutedEventArgs e)
        {
            MainWindow.IsCashier = true;
            Sales1 window3 = new Sales1();
            window3.Show();
            Close();
        }

        public void Click_to_refund_cashier(object sender, RoutedEventArgs e)
        {
            MainWindow.IsCashier = true;
            Refund_of_products window5 = new Refund_of_products();
            window5.Show();
            Close();
        }

        public void Click_to_statement_cashier(object sender, RoutedEventArgs e)
        {
            MainWindow.IsCashier = true;
            Statements window6 = new Statements();
            window6.Show();
            Close();
        }

        public void Click_to_calculator(object sender, RoutedEventArgs e)
        {
            MainWindow.IsCashier = true;
            Calculator window8 = new Calculator();
            window8.Show();
            Close();
        }

        private void Click_to_replace(object sender, RoutedEventArgs e)
        {
            MainWindow Cash_register = new MainWindow();
            Cash_register.Show();
            Close();
        }
    }
}
