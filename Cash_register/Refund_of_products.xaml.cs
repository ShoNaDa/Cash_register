using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Refund_of_products.xaml
    /// </summary>
    public partial class Refund_of_products : Window
    {
        List<string> Numbers = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        List<string> ListOfProductsSold = new List<string>(1);
        public Refund_of_products()
        {
            InitializeComponent();

            DataTable dt_products = Select("Select * from ProductsSold");
            for (int i = 0; i < dt_products.Rows.Count; i++)
            {
                List_of_products_sold.Items.Add("Номер чека: " + Convert.ToString(dt_products.Rows[i][0]) + ". " + Convert.ToString(dt_products.Rows[i][1]).Replace('?', '₽'));
            }

            for (int i = 0; i < List_of_products_sold.Items.Count; i++)
            {
                ListOfProductsSold.Add(Convert.ToString(List_of_products_sold.Items[i]));
            }
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
            Button_searching.Visibility = Visibility.Visible;
        }

        public DataTable Select(string selectSQL) // функция подключения к базе данных и обработка запросов
        {
            DataTable dataTable = new DataTable("dataBase");                // создаём таблицу в приложении
                                                                            // подключаемся к базе данных
            SqlConnection sqlConnection = new SqlConnection(@"server=WIN-PA0KKAO063F\SQLEXPRESS;Trusted_Connection=Yes;DataBase=Cash_register;");
            sqlConnection.Open();                                           // открываем базу данных
            SqlCommand sqlCommand = sqlConnection.CreateCommand();          // создаём команду
            sqlCommand.CommandText = selectSQL;                             // присваиваем команде текст
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); // создаём обработчик
            sqlDataAdapter.Fill(dataTable);                                 // возращаем таблицу с результатом
            sqlConnection.Close();
            return dataTable;
        }

        private void Click_searching(object sender, RoutedEventArgs e)
        {
            bool IsOk = false;
            for (int i = 0; i < Search_cheque.Text.Length; i++)
            {
                if (IsOk)
                {
                    IsOk = false;
                }
                for (int j = 0; j < Numbers.Count; j++)
                {
                    if (Convert.ToString(Search_cheque.Text[i]).Contains(Numbers[j]))
                    {
                        IsOk = true;
                        break;
                    }
                }
                if (IsOk == false)
                {
                    break;
                }
            }

            List_of_products_sold.Items.Clear();
            for (int i = 0; i < ListOfProductsSold.Count; i++)
            {
                if (IsOk)
                {
                    if (Convert.ToString(ListOfProductsSold[i]).Split(':')[1].Split('.')[0].Trim().Contains(Search_cheque.Text.Trim()))
                    {
                        List_of_products_sold.Items.Add(ListOfProductsSold[i]);
                    }
                }
            }
        }

        private void Click_to_refund_product(object sender, RoutedEventArgs e)
        {
            if (List_of_products_sold.SelectedIndex != -1)
            {
                //Возвращаем проданное количество
                string count = Convert.ToString(List_of_products_sold.SelectedItem).Split('(')[1].Split(' ')[0].Trim();
                string receiptNumber = Convert.ToString(List_of_products_sold.SelectedItem).Split(':')[1].Split('.')[0].Trim();
                DataTable dt_id = Select("Select FK_ProductId from ProductsSold where ReceiptNumber = " + receiptNumber);
                DataTable dt_products = Select("Update Products set ProductCount = ProductCount + " + count + " where ProductId = " + Convert.ToString(dt_id.Rows[0][0]));
                //Добавляем возврат
                DataTable dt_sale = Select("Update Statements set Refund = Refund + " + Convert.ToString(List_of_products_sold.SelectedItem).Split(':')[2].Split('₽')[0].Trim().Replace(',', '.') + " where StatementsId = (Select count(StatementsId) from Statements)");
                //Удаляем из списка
                DataTable dt_sold = Select("Delete from ProductsSold where ReceiptNumber = " + receiptNumber);
                Refund_of_products window5 = new Refund_of_products();
                window5.Show();
                Close();
            }
        }
    }
}