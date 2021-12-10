using System;
using System.Collections.Generic;
using System.Data;
using static Cash_register.SQLRequest;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls.Primitives;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Count.xaml
    /// </summary>
    public partial class Count : Window
    {
        //List
        List<string> Numbers = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        //int
        public static int countOfProduct;

        public Count()
        {
            InitializeComponent();
        }

        private void Click_go_next(object sender, RoutedEventArgs e)
        {
            bool сountIsOk = false;

            //если все ок
            if (CountIsOk(count_product.Text, сountIsOk, Numbers))
            {
                bool countNotMore = true;

                DataTable dt_product = SQLrequest("Select ProductCount from Products where ProductId = " + Convert.ToInt32(Products_sale.idProductForCount));

                //проверяем: есть ли столько товара на складе
                if (CountNotMore(countNotMore, Convert.ToInt32(dt_product.Rows[0][0]), Convert.ToInt32(count_product.Text)))
                {
                    //создаем строку продажи товара после ввода количества
                    countOfProduct = Convert.ToInt32(count_product.Text);
                    Sales1.ProductsSale[Sales1.ProductsSale.Count - 1] = "Код: " + Convert.ToString(Products_sale.idProductForCount) + ". " + Products_sale.nameProduct + ". Стоимость: " + countOfProduct * Products_sale.price + "₽ (" + countOfProduct + " шт)";

                    Sales1 sales = new Sales1();
                    sales.Show();
                    Close();
                }
            }
        }

        //проверяет некоторую валидацию
        public static bool CountIsOk(string countProduct, bool countIsOk, List<string> Numbers)
        {
            //проверяем нет ли пустых полей
            if (countProduct != "")
            {
                //проверяем правильно ли написано количество
                for (int i = 0; i < countProduct.Length; i++)
                {
                    if (countIsOk)
                    {
                        countIsOk = false;
                    }

                    for (int j = 0; j < Numbers.Count; j++)
                    {
                        if (Convert.ToString(countProduct[i]).Contains(Numbers[j]))
                        {
                            countIsOk = true;
                            break;
                        }
                    }

                    if (!countIsOk)
                    {
                        break;
                    }
                }
            }

            //если все ок
            if (countIsOk)
            {
                //проверяем: количество больше нуля?
                if (Convert.ToInt32(countProduct) <= 0)
                {
                    countIsOk = false;
                    MessageBox.Show("Неправильный формат");
                }
            }
            else
            {
                MessageBox.Show("Неправильный формат");
            }

            return countIsOk;
        }

        //функция - проверка: столько товара есть на складе?
        public static bool CountNotMore(bool countNotMore, int countInDB, int countEntered)
        {
            if (countInDB < countEntered)
            {
                countNotMore = false;
                MessageBox.Show("Такого количества товара нет на складе");
            }

            return countNotMore;
        }

        private void Click_back(object sender, RoutedEventArgs e)
        {
            Sales1.ProductsSale.Clear();
            
            Products_sale sale = new Products_sale();
            sale.Show();
            Close();
        }

        private void Count_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Button_back.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            if (e.Key == Key.Enter)
            {
                Button_go_next.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
    }
}
