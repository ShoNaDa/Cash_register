using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Sales1.xaml
    /// </summary>
    public partial class Sales1 : Window
    {
        public static List<string> ProductsSale = new List<string>(1);
        public static List<string> SalesList = new List<string>(1);
        public double result;
        public Sales1()
        {
            InitializeComponent();

            foreach (string i in ProductsSale)
            {
                List_of_product_sale.Items.Add(i);
            }
            
            if (List_of_product_sale.Items.Count != 0)
            {
                ToPay.Opacity = 1;
                toPay.Opacity = 1;
                Result.Opacity = 1;
                for (int i = 0; i < List_of_product_sale.Items.Count; i++)
                {
                    Result.Text = "";
                    result += Convert.ToDouble(Convert.ToString(List_of_product_sale.Items[i]).Split('₽')[0].Split(':')[2].Trim());
                }
                Result.Text = Convert.ToString(result + " ₽");
            }
            else
            {
                ToPay.Opacity = 0.5;
                toPay.Opacity = 0.5;
                Result.Opacity = 0.5;
            }
        }

        public void Click_to_be_paid(object sender, RoutedEventArgs e)
        {
            if (List_of_product_sale.Items.Count != 0)
            {
                MessageBox.Show("Оплачено");
                for (int i = 0; i < List_of_product_sale.Items.Count; i++)
                {
                    string count = Convert.ToString(List_of_product_sale.Items[i]).Split('(')[1].Split(' ')[0].Trim();
                    string id = Convert.ToString(List_of_product_sale.Items[i]).Split(':')[1].Split('.')[0].Trim();
                    DataTable dt_products = Select("Update Products set ProductCount = ProductCount - " + count + " where ProductId = " + id);
                    DataTable dt_products_sold = Select("Insert into ProductsSold values ('" + Convert.ToString(List_of_product_sale.Items[i]).Substring(Convert.ToString(List_of_product_sale.Items[i]).IndexOf('.') + 2).Replace(',', '.') + "', " + id + ")");
                }
                DataTable statementsId = Select("Select count(StatementsId) from Statements");
                DataTable salesInStatement = Select("Update Statements set Sales = Sales + " + Convert.ToDouble(Result.Text.Split(' ')[0].Trim().Replace(',', '.')) + "where StatementsId = " + Convert.ToInt32(statementsId.Rows[0][0]));
                ProductsSale.Clear();
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
            else
            {
                MessageBox.Show("Товары не выбраны");
            }
        }

        public void Click_add_product(object sender, RoutedEventArgs e)
        {
            Products_sale Product_sale = new Products_sale();
            Product_sale.Show();
            Close();
        }

        public void Click_back(object sender, RoutedEventArgs e)
        {
            ProductsSale.Clear();
            if (MainWindow.IsCashier)
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
    }
}