using System.Windows;
using System.Windows.Controls;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Cheque.xaml
    /// </summary>
    public partial class Cheque : Window
    {
        public Cheque()
        {
            InitializeComponent();

            cheque.Text = "\t\tТоварный чек\n";
            int count = 1;
            foreach (string item in Sales1.cheque)
            {
                cheque.Text += count + ") " + item + "\n";
                count++;
            }
            cheque.Text += "\tИТОГ: " + Sales1.fullPrice;
        }

        private void Click_back(object sender, RoutedEventArgs e)
        {
            if (MainWindow.IsCashier)
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

        private void Click_print(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(cheque, "Printing");
            }
            if (MainWindow.IsCashier)
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
    }
}
