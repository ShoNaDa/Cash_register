using System;
using System.Collections.Generic;
using System.Data;
using static Cash_register.SQLRequest;
using static Cash_register.ChekingValidityProduct;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls.Primitives;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Add_product.xaml
    /// </summary>
    public partial class Add_product : Window
    {
        //List
        private readonly List<string> Signs = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", ".", "," };
        private readonly List<string> Numbers = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

        public Add_product()
        {
            InitializeComponent();

            //с этой штукой правильно работает точка и запятая
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            //Заполняем поле код (цифрой, которая идет после последнего ID в БД)
            DataTable dt_productId = SQLrequest("Select max(ProductId) from Products");

            //тернарный оператор
            product_code.Text = Convert.ToString(dt_productId.Rows[0][0]) == "" ? "1" : Convert.ToString((int)dt_productId.Rows[0][0] + 1);

            add_product_name.Focus();
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
                bool сountIsOk = false;
                bool priceIsOk = false;
                bool nameIsOk = true;

                //если все ок и цена и количество больше нуля
                if (ValidIsOk(add_count.Text, сountIsOk, Numbers) &&
                    ValidIsOk(add_price.Text, priceIsOk, Signs) &&
                    PriceValid(add_price.Text, priceIsOk) &&
                    NameIsOk(add_product_name.Text, nameIsOk) &&
                    Convert.ToInt32(add_count.Text) > 0 && Convert.ToDouble(add_price.Text) > 0)
                {
                    SQLrequest("Insert into [dbo].[Products] values " + "('" + add_product_name.Text +
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
                MessageBox.Show("Все строки должны быть заполнены");
            }
        }

        private void Window16_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Button_back.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            if (e.Key == Key.Enter && add_product_name.Focusable != true && add_price.Focusable != true && add_count.Focusable == true)
            {
                Button_add_product.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            else if (e.Key == Key.Enter && add_product_name.Focusable == true)
            {
                add_product_name.Focusable = false;
                add_price.Focus();
            }
            else if (e.Key == Key.Enter && add_price.Focusable == true)
            {
                add_price.Focusable = false;
                add_count.Focus();
            }
        }
    }
}