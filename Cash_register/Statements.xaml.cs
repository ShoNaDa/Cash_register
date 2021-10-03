using System;
using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Statements.xaml
    /// </summary>
    public partial class Statements : Window
    {
        public Statements()
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

        public void Click_to_deposit(object sender, RoutedEventArgs e)
        {
            MainWindow.IsDeposit = true;
            Amount_of_money Introduction = new Amount_of_money();
            Introduction.Introduction.Visibility = Visibility.Visible;
            Amount_of_money window10 = new Amount_of_money();
            window10.Show();
            Close();
        }

        public void Click_to_withdraw(object sender, RoutedEventArgs e)
        {
            MainWindow.IsDeposit = false;
            Amount_of_money Payment = new Amount_of_money();
            Payment.Introduction.Visibility = Visibility.Visible;
            Amount_of_money window10 = new Amount_of_money();
            window10.Show();
            Close();
        }

        public void Click_to_close_shift(object sender, RoutedEventArgs e)
        {

        }
    }
}
