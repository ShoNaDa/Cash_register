using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Product_search.xaml
    /// </summary>
    public partial class Product_search : Window
    {
        public static string nameProduct;
        public static int idProduct;
        public static double salePrice;
        public Product_search()
        {
            InitializeComponent();
            DataTable dt_products = Select("SELECT * FROM [dbo].[Products]");
            for (int i = 0; i < dt_products.Rows.Count; i++)
            {
                MainWindow.Products.Add("Код: " + Convert.ToString(dt_products.Rows[i][0]) + ". " + (string)dt_products.Rows[i][1] + " (" + Convert.ToString(Convert.ToDouble(dt_products.Rows[i][2])) + "₽)");
            }
            
            foreach (string i in MainWindow.Products)
            {
                List_of_products.Items.Add(i);
            }
        }

        public void Click_add_product_plus(object sender, RoutedEventArgs e)
        {
            MainWindow.Products.Clear();
            Add_product window16 = new Add_product();
            window16.Show();
            Close();
        }

        public void Click_back(object sender, RoutedEventArgs e)
        {
            MainWindow.Products.Clear();
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
            MainWindow.Products.Clear();
            Button_find_a_product.Visibility = Visibility.Hidden;
            Search_product.Visibility = Visibility.Visible;
        }

        private void List_of_products_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

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

        private void Click_to_drop_product(object sender, RoutedEventArgs e)
        {
            if (List_of_products.SelectedIndex != -1)
            {
                int id = Convert.ToInt32(Convert.ToString(List_of_products.SelectedItem).Split(' ', '.')[1]);
                DataTable dt_product = Select("delete from [dbo].[Products] WHERE ProductId = " + id);
                MessageBox.Show("Данные успешно удалены");
                MainWindow.Products.Clear();
                After_logging_in window2 = new After_logging_in();
                window2.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Выберите товар");
            }
        }

        private void Click_to_edit_product(object sender, RoutedEventArgs e)
        {
            if (List_of_products.SelectedIndex != -1)
            {
                nameProduct = Convert.ToString(List_of_products.SelectedItem).Split(' ')[2];
                idProduct = Convert.ToInt32(Convert.ToString(List_of_products.SelectedItem).Split(' ', '.')[1]);
                salePrice = Convert.ToDouble(Convert.ToString(List_of_products.SelectedItem).Split(' ', '₽')[3].Substring(1));
                MainWindow.Products.Clear();
                Edit_product edit_product = new Edit_product();
                edit_product.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Выберите товар");
            }
        }
    }
}
