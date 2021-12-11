using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using static Cash_register.SQLRequest;

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

            //Сделали шаблон чека
            cheque.Text = "\t\tТоварный чек\n";

            int count = 1;

            foreach (string item in Sales1.cheque)
            {
                cheque.Text += count + ") " + item + "\n";
                count++;
            }

            cheque.Text += "\tИТОГ: " + Sales1.fullPrice + "₽";

            //ID этой продажи
            DataTable dt_SaleId = SQLrequest("Select SaleId from Sale where SaleId = (select count(SaleId) from Sale)");
            int saleId = Convert.ToInt32(dt_SaleId.Rows[0][0]);
            
            //ID чека
            DataTable dt_chequeId = SQLrequest("Select count(ChequeId) from Cheque");
            int chequeId = Convert.ToInt32(dt_chequeId.Rows[0][0]) + 1;
            
            //добавили чек в БД
            SQLrequest("Insert into Cheque values (" + chequeId + ", " + saleId + ")");
            Sales1.cheque.Clear();
        }

        private void Click_back(object sender, RoutedEventArgs e)
        {
            if (MainWindow.isCashier)
            {
                After_login_in_cashier window9 = new After_login_in_cashier();
                window9.Show();
                Close();
            }
            else
            {
                Sales1.cheque.Clear();

                After_logging_in window2 = new After_logging_in();
                window2.Show();
                Close();
            }
        }

        private void Click_print(object sender, RoutedEventArgs e)
        {
            //создаем окно диалога принтера
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                //при выборе принтера печатаем или если выбрали сохранить в PDF - сохраняем в PDF
                printDialog.PrintVisual(cheque, "Printing");
            }

            if (MainWindow.isCashier)
            {
                After_login_in_cashier window9 = new After_login_in_cashier();
                window9.Show();
                Close();
            }
            else
            {
                Sales1.cheque.Clear();

                After_logging_in window2 = new After_logging_in();
                window2.Show();
                Close();
            }
        }
    }
}
