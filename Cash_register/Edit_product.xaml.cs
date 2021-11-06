using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Edit_product.xaml
    /// </summary>
    public partial class Edit_product : Window
    {
        List<string> Signs = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", ".", "," };
        List<string> Numbers = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

        public Edit_product()
        {
            InitializeComponent();

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            edit_product_name.Text = Product_search.nameProduct;
            product_code_edit.Text = Convert.ToString(Product_search.idProduct);
            edit_price.Text = Convert.ToString(Product_search.salePrice);
            edit_count.Text = Convert.ToString(Product_search.productCount);
        }

        private void Click_back(object sender, RoutedEventArgs e)
        {
            Product_search window4 = new Product_search();
            window4.Show();
            Close();
        }

        private void Click_to_save_product(object sender, RoutedEventArgs e)
        {
            //проверяем нет ли пустых полей
            if (edit_product_name.Text != "" && edit_price.Text != "" && edit_count.Text != "")
            {
                bool CountIsOk = false;
                bool PriceIsOk = false;
                bool NameIsOk = true;
                //проверяем правильно ли написано количество
                for (int i = 0; i < edit_count.Text.Length; i++)
                {
                    if (CountIsOk)
                    {
                        CountIsOk = false;
                    }
                    for (int j = 0; j < Numbers.Count; j++)
                    {
                        if (Convert.ToString(edit_count.Text[i]).Contains(Numbers[j]))
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
                for (int i = 0; i < edit_price.Text.Length; i++)
                {
                    if (PriceIsOk)
                    {
                        PriceIsOk = false;
                    }
                    for (int j = 0; j < Signs.Count; j++)
                    {
                        if (Convert.ToString(edit_price.Text[i]).Contains(Signs[j]))
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
                //проверяем правильно ли написано название
                for (int i = 0; i < edit_product_name.Text.Length; i++)
                {
                    if (edit_product_name.Text[i] == '-' || edit_product_name.Text[i] == '(' || edit_product_name.Text[i] == '?')
                    {
                        MessageBox.Show("Нельзя использовать '-' / '(' / '?' в названии");
                        NameIsOk = false;
                    }
                }
                //если все ок и цена не начинается и не заканчивается с "." или ","
                if (CountIsOk && PriceIsOk && NameIsOk &&
                    Convert.ToString(edit_price.Text[0]) != "." && Convert.ToString(edit_price.Text[0]) != "," &&
                    Convert.ToString(edit_price.Text[edit_price.Text.Length - 1]) != "." && Convert.ToString(edit_price.Text[edit_price.Text.Length - 1]) != ",")
                {
                    //проверяем: цена и количество больше нуля?
                    if (Convert.ToInt32(edit_count.Text) > 0 && Convert.ToDouble(edit_price.Text) > 0)
                    {
                        DataTable dt_product = Update("update [dbo].[Products] set ProductName = '" + edit_product_name.Text
                                                                                                   + "', SalePrice = " + Convert.ToDouble(Convert.ToString(edit_price.Text).Replace(',', '.'))
                                                                                                   + ", ProductCount = " + Convert.ToInt32(edit_count.Text)
                                                                                                   + " WHERE ProductId = " + Convert.ToInt32(product_code_edit.Text));
                        MessageBox.Show("Данные успешно изменены");
                        After_logging_in window2 = new After_logging_in();
                        window2.Show();
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

        public DataTable Update(string selectSQL) // функция подключения к базе данных и обработка запросов
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