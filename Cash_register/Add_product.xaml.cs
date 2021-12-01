using System;
using System.Collections.Generic;
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
        List<string> Signs = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", ".", "," };
        List<string> Numbers = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

        public Add_product()
        {
            InitializeComponent();
            //с этой штукой правильно работает точка и запятая
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            try
            {
                DataTable dt_productId = Insert("select max(ProductId) from Products");
                if (Convert.ToString(dt_productId.Rows[0][0]) == "")
                {
                    product_code.Text = "1";
                }
                else
                {
                    product_code.Text = Convert.ToString((int)dt_productId.Rows[0][0] + 1);
                }
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
            //проверяем нет ли пустых полей
            if (add_product_name.Text != "" && add_price.Text != "" && add_count.Text != "")
            {
                bool CountIsOk = false;
                bool PriceIsOk = false;
                bool NameIsOk = true;
                //проверяем правильно ли написано количество
                for (int i = 0; i < add_count.Text.Length; i++)
                {
                    if (CountIsOk)
                    {
                        CountIsOk = false;
                    }
                    for (int j = 0; j < Numbers.Count; j++)
                    {
                        if (Convert.ToString(add_count.Text[i]).Contains(Numbers[j]))
                        {
                            CountIsOk = true;
                            break;
                        }
                    }
                    if (CountIsOk == false)
                    {
                        break;
                    }
                }
                //проверяем правильно ли написана цена
                for (int i = 0; i < add_price.Text.Length; i++)
                {
                    if (PriceIsOk)
                    {
                        PriceIsOk = false;
                    }
                    for (int j = 0; j < Signs.Count; j++)
                    {
                        if (Convert.ToString(add_price.Text[i]).Contains(Signs[j]))
                        {
                            PriceIsOk = true;
                            break;
                        }
                    }
                    if (PriceIsOk == false)
                    {
                        break;
                    }
                }
                int count = 0;
                for (int i = 0; i < add_price.Text.Length; i++)
                {
                    if (Convert.ToString(add_price.Text[i]) == "." || Convert.ToString(add_price.Text[i]) == ",")
                    {
                        count++;
                        if (count == 2)
                        {
                            PriceIsOk = false;
                            break;
                        }
                    }
                }
                //проверяем правильно ли написано название
                for (int i = 0; i < add_product_name.Text.Length; i++)
                {
                    if (add_product_name.Text[i] == '-' || add_product_name.Text[i] == '(' || add_product_name.Text[i] == '?')
                    {
                        MessageBox.Show("Нельзя использовать '-' / '(' / '?' в названии");
                        NameIsOk = false;
                    }
                }
                //если все ок и цена не начинается и не заканчивается с "." или ","
                if (CountIsOk && PriceIsOk && NameIsOk &&
                    Convert.ToString(add_price.Text[0]) != "." && Convert.ToString(add_price.Text[0]) != "," &&
                    Convert.ToString(add_price.Text[add_price.Text.Length - 1]) != "." && Convert.ToString(add_price.Text[add_price.Text.Length - 1]) != ",")
                {
                    //проверяем: цена и количество больше нуля?
                    if (Convert.ToInt32(add_count.Text) > 0 && Convert.ToDouble(add_price.Text) > 0)
                    {
                        DataTable dt_product = Insert("Insert into [dbo].[Products] values " +
                                                                              "('" + add_product_name.Text +
                                                                            "', " + Convert.ToDouble(Convert.ToString(add_price.Text).Replace(',', '.')) +
                                                                            ", " + Convert.ToInt32(add_count.Text) + ")");
                        MessageBox.Show("Продукт успешно добавлен");
                        Product_search window4 = new Product_search();
                        window4.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Неправильный формат");
                    }
                }
                else
                {
                    MessageBox.Show("Неправильный формат");
                }
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