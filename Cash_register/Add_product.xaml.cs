using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Add_product.xaml
    /// </summary>
    public partial class Add_product : Window
    {
        public Add_product()
        {
            InitializeComponent();

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            try
            {
                DataTable dt_productId = Insert("select max(ProductId) from Products");
                product_code.Text = Convert.ToString((int)dt_productId.Rows[0][0] + 1);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void Click_back(object sender, RoutedEventArgs e)
        {
            Product_search window4 = new Product_search();
            window4.Show();
            Close();
        }

        private void Click_to_add_product(object sender, RoutedEventArgs e)
        {
            if (add_product_name.Text != "" && add_price.Text != "")
            {
                DataTable dt_product = Insert("Insert into [dbo].[Products] values " +
                                                                          "('" + add_product_name.Text +
                                                                        "', " + Convert.ToDouble(Convert.ToString(add_price.Text).Replace(',', '.')) + ")");
                MessageBox.Show("Продукт успешно добавлен");
                Product_search window4 = new Product_search();
                window4.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Все строки должны быть заполнены");
            }
        }

        public DataTable Insert(string selectSQL) // функция подключения к базе данных и обработка запросов
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
