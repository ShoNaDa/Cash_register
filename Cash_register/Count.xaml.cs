﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Count.xaml
    /// </summary>
    public partial class Count : Window
    {
        List<string> Numbers = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        public static int countOfProduct;
        public Count()
        {
            InitializeComponent();
        }

        private void Click_go_next(object sender, RoutedEventArgs e)
        {
            //проверяем нет ли пустых полей
            if (count_product.Text != "")
            {
                bool CountIsOk = false;
                //проверяем правильно ли написано количество
                for (int i = 0; i < count_product.Text.Length; i++)
                {
                    if (CountIsOk)
                    {
                        CountIsOk = false;
                    }
                    for (int j = 0; j < Numbers.Count; j++)
                    {
                        if (Convert.ToString(count_product.Text[i]).Contains(Numbers[j]))
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
                //если все ок
                if (CountIsOk)
                {
                    //проверяем: количество больше нуля?
                    if (Convert.ToInt32(count_product.Text) > 0)
                    {
                        DataTable dt_product = Insert("Select ProductCount from Products where ProductId = " + Convert.ToInt32(Products_sale.idProductForCount));
                        if (Convert.ToInt32(dt_product.Rows[0][0]) >= Convert.ToInt32(count_product.Text))
                        {
                            countOfProduct = Convert.ToInt32(count_product.Text);
                            Sales1.ProductsSale[Sales1.ProductsSale.Count - 1] = "Код: " + Convert.ToString(Products_sale.idProductForCount) + ". " + Products_sale.nameProduct + ". Стоимость: " + countOfProduct * Products_sale.price + "₽";
                            Sales1 sales = new Sales1();
                            sales.Show();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Такого количества товара нет на складе");
                        }
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
