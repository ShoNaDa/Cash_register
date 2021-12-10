using System;
using System.Data;
using static Cash_register.SQLRequest;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using System.Collections.Generic;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Products_sale.xaml
    /// </summary>
    public partial class Products_sale : Window
    {
        //int
        public static int idProductForCount;
        //double
        public static double price;
        //string
        public static string nameProduct;

        public Products_sale()
        {
            InitializeComponent();

            //Создаем список всех продуктов
            DataTable dt_products = SQLrequest("SELECT * FROM [dbo].[Products]");
            for (int i = 0; i < dt_products.Rows.Count; i++)
            {
                MainWindow.Products.Add("Код: " + Convert.ToString(dt_products.Rows[i][0]) + ". " + Convert.ToString(dt_products.Rows[i][1]) + " - " + Convert.ToString(dt_products.Rows[i][3]) + " шт (" + Convert.ToString(Convert.ToDouble(dt_products.Rows[i][2])) + "₽)");
            }

            foreach (string i in MainWindow.Products)
            {
                List_of_products.Items.Add(i);
            }
        }
        private void Click_to_find_product(object sender, RoutedEventArgs e)
        {
            //если нажимаем поиск, то открываем текстбокс
            Button_find_product.Visibility = Visibility.Hidden;
            Search_product.Visibility = Visibility.Visible;
            Button_search.Visibility = Visibility.Visible;

            Search_product.Focus();
        }

        private void Click_to_add_product_sale(object sender, RoutedEventArgs e)
        {
            MainWindow.Products.Clear();

            if (List_of_products.SelectedIndex != -1)
            {
                bool ProductContain = false;

                for (int i = 0; i < Sales1.ProductsSale.Count; i++)
                {
                    //проверяем добавили ли мы товар в продажи
                    if (Sales1.ProductsSale[i].Split(' ', '.')[1] == Convert.ToString(List_of_products.SelectedItem).Split(' ', '.')[1])
                    {
                        ProductContain = true;

                        MessageBox.Show("Этот товар уже добавлен в продажи");

                        break;
                    }
                }

                //если еще не добавлен
                if (!ProductContain)
                {
                    //добавляем в лист продаваемых товаров
                    Sales1.ProductsSale.Add(Convert.ToString(List_of_products.SelectedItem));

                    //вытаскиваем название товара
                    nameProduct = Convert.ToString(List_of_products.SelectedItem).Split('.')[1].Split('-')[0].Trim();
                    
                    //вытаскиваем ID
                    idProductForCount = Convert.ToInt32(Convert.ToString(List_of_products.SelectedItem).Split(' ', '.')[1]);
                    
                    //вытаскиваем цену товара
                    price = Convert.ToDouble(Convert.ToString(List_of_products.SelectedItem).Split('(')[1].Split('₽')[0]);

                    Count count = new Count();
                    count.Show();
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Выберите товар");
            }
        }

        private void Click_back(object sender, RoutedEventArgs e)
        {
            MainWindow.Products.Clear();

            Sales1 window9 = new Sales1();
            window9.Show();
            Close();
        }

        private void Click_search(object sender, RoutedEventArgs e)
        {
            List_of_products.Items.Clear();

            //создаем лист для заполнения искомыми товарами
            List<string> ListOfProduct = new List<string>();
            
            //искает...
            ListOfProducts(MainWindow.Products, Search_product.Text, ListOfProduct);

            //выводим в окошко искомые товары
            foreach(string item in ListOfProduct)
            {
                List_of_products.Items.Add(item);
            }
        }

        //функция поиска
        public static List<string> ListOfProducts(List<string> Products, string searchProduct, List<string> ListOfProducts)
        {
            //ищет любые вхождения подстроки в строках
            foreach (string i in Products)
            {
                if (i.Contains(searchProduct.Trim()) || i.ToLower().Contains(searchProduct.Trim()))
                {
                    ListOfProducts.Add(i);
                }
            }

            return ListOfProducts;
        }

        private void Product_sale_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Button_back.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            if (e.Key == Key.Enter && Search_product.Focusable != true)
            {
                Button_add_product_sale.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }

        private void Search_product_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Click_search(sender, e);
            }
        }
    }
}
