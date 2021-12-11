using System;
using System.Collections.Generic;
using static Cash_register.SQLRequest;
using static Cash_register.ChekingValidityProduct;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls.Primitives;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Edit_product.xaml
    /// </summary>
    public partial class Edit_product : Window
    {
        //List
        private readonly List<string> Signs = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", ".", "," };
        private readonly List<string> Numbers = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

        public Edit_product()
        {
            InitializeComponent();

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            //вставляем в текстблоки информацию о продукте, который хотим изменить
            edit_product_name.Text = Product_search.nameProduct;
            product_code_edit.Text = Convert.ToString(Product_search.idProduct);
            edit_price.Text = Convert.ToString(Product_search.salePrice);
            edit_count.Text = Convert.ToString(Product_search.productCount);

            edit_product_name.Focus();
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
                bool сountIsOk = false;
                bool priceIsOk = false;
                bool nameIsOk = true;

                //если все ок и цена и количество больше нуля
                if (ValidIsOk(edit_count.Text, сountIsOk, Numbers) &&
                    ValidIsOk(edit_price.Text, priceIsOk, Signs) &&
                    PriceValid(edit_price.Text, priceIsOk) &&
                    NameIsOk(edit_product_name.Text, nameIsOk) &&
                    Convert.ToInt32(edit_count.Text) > 0 && Convert.ToDouble(edit_price.Text) > 0)
                {
                    SQLrequest("Update [dbo].[Products] set ProductName = '" + edit_product_name.Text
                                                                                 + "', ProductPrice = " + Convert.ToDouble(Convert.ToString(edit_price.Text).Replace(',', '.'))
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
                MessageBox.Show("Все строки должны быть заполнены");
            }
        }

        private void Edit_product_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Button_back.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            if (e.Key == Key.Enter && edit_product_name.Focusable != true && edit_price.Focusable != true && edit_count.Focusable == true)
            {
                Button_save_product.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            else if (e.Key == Key.Enter && edit_product_name.Focusable == true)
            {
                edit_product_name.Focusable = false;
                edit_price.Focus();
            }
            else if (e.Key == Key.Enter && edit_price.Focusable == true)
            {
                edit_price.Focusable = false;
                edit_count.Focus();
            }
        }
    }
}