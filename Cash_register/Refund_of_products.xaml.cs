﻿using System;
using System.Collections.Generic;
using System.Data;
using static Cash_register.SQLRequest;
using static Cash_register.DateFunc;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls.Primitives;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Refund_of_products.xaml
    /// </summary>
    public partial class Refund_of_products : Window
    {
        List<string> Numbers = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        List<string> ListOfProductsSold = new List<string>(1);
        static List<int> numberOfChequeProductSold = new List<int>();

        public static Dictionary<int, double> refunds = new Dictionary<int, double>();

        public static double refundOfShift = 0;

        public Refund_of_products()
        {
            InitializeComponent();

            //с этой штукой правильно работает точка и запятая
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            DataTable dt_ProductsList = SQLrequest("Select * from ProductsList");

            for (int i = 0; i < dt_ProductsList.Rows.Count; i++)
            {
                //ID проданного товара
                DataTable dt_productId = SQLrequest("Select FK_ProductId from ProductsList where ProductsListId = " + Convert.ToInt32(dt_ProductsList.Rows[i][0]));
                //его название и цена 
                DataTable dt_productsInfo = SQLrequest("Select ProductName, ProductPrice from Products where ProductId = " + Convert.ToInt32(dt_productId.Rows[0][0]));
                //количество проданного товара
                DataTable dt_count = SQLrequest("Select ProductCount from ProductsList where ProductsListId = " + Convert.ToInt32(dt_ProductsList.Rows[i][0]));
                //сумма
                double sum = Convert.ToDouble(dt_productsInfo.Rows[0][1]) * Convert.ToDouble(dt_count.Rows[0][0]);

                //проверяем вернули ли мы такой товар
                bool isRefund = false;
                foreach (int item in numberOfChequeProductSold)
                {
                    if (Convert.ToInt32(dt_ProductsList.Rows[i][0]) == item)
                    {
                        isRefund = true;
                    }
                }
                if (!isRefund)
                {
                    //прописываем проданные товары
                    List_of_products_sold.Items.Add("Номер чека: " + Convert.ToString(dt_ProductsList.Rows[i][0]) + ". "
                        + Convert.ToString(dt_productsInfo.Rows[0][0])
                        + " (Цена: " + Convert.ToString(Convert.ToDouble(dt_productsInfo.Rows[0][1])) + "₽). Количество: " + Convert.ToString(dt_count.Rows[0][0]) + ", Сумма: " + Convert.ToString(sum));
                }
            }

            for (int i = 0; i < List_of_products_sold.Items.Count; i++)
            {
                ListOfProductsSold.Add(Convert.ToString(List_of_products_sold.Items[i]));
            }
        }

        public void Click_back(object sender, RoutedEventArgs e)
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

        public void Click_to_find_a_cheque(object sender, RoutedEventArgs e)
        {
            Button_find_a_cheque.Visibility = Visibility.Hidden;
            Search_cheque.Visibility = Visibility.Visible;
            Button_searching.Visibility = Visibility.Visible;

            Search_cheque.Focus();
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
                //количество возвращенного товара
                string count = Convert.ToString(List_of_products_sold.SelectedItem).Split(':')[3].Split(',')[0].Trim();
                //ID возвращенного товара
                string idInList = Convert.ToString(List_of_products_sold.SelectedItem).Split(':')[1].Split('.')[0].Trim();
                DataTable dt_id = SQLrequest("Select FK_ProductId from ProductsList where ProductsListId = " + idInList);
                //сумма 
                string amount = Convert.ToString(List_of_products_sold.SelectedItem).Split(':')[4].Trim().Replace(',', '.');

                SQLrequest("Update Products set ProductCount = ProductCount + " + count + " where ProductId = " + dt_id.Rows[0][0]);

                //Добавляем возврат
                SQLrequest("Insert into Refunds values (" + dt_id.Rows[0][0] + ", " + count + ", " + amount + ", '" + DateFunction()[2] + DateFunction()[0] + DateFunction()[1] + "')");

                refundOfShift += Convert.ToDouble(amount);

                //если это первый возврат за смену
                if (refunds.ContainsKey(Convert.ToInt32(SQLrequest("Select max(ShiftId) from [Shift]").Rows[0][0])))
                {
                    refunds[Convert.ToInt32(SQLrequest("Select max(ShiftId) from [Shift]").Rows[0][0])] += Convert.ToDouble(amount);
                }
                else
                {
                    refunds.Add(Convert.ToInt32(SQLrequest("Select max(ShiftId) from [Shift]").Rows[0][0]), Convert.ToDouble(amount));
                }
                numberOfChequeProductSold.Add(Convert.ToInt32(Convert.ToString(List_of_products_sold.SelectedItem).Split(' ')[2].Split('.')[0].Trim()));

                Refund_of_products window5 = new Refund_of_products();
                window5.Show();
                Close();
            }
        }

        private void Window5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Button_back.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            if (e.Key == Key.Enter && List_of_products_sold.SelectedIndex != -1)
            {
                Button_refund_product.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }

        private void Search_cheque_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Click_searching(sender, e);
            }
        }
    }
}