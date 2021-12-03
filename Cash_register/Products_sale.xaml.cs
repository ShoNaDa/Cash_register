using System;
using System.Data;
using static Cash_register.SQLRequest;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls.Primitives;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Products_sale.xaml
    /// </summary>
    public partial class Products_sale : Window
    {
        public static int idProductForCount;
        public static double price;
        public static string nameProduct;

        public Products_sale()
        {
            InitializeComponent();
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
                    if (Sales1.ProductsSale[i].Split(' ', '.')[1] == Convert.ToString(List_of_products.SelectedItem).Split(' ', '.')[1])
                    {
                        ProductContain = true;
                        MessageBox.Show("Этот товар уже добавлен в продажи");
                        break;
                    }
                }
                if (ProductContain == false)
                {
                    try
                    {
                        Sales1.ProductsSale.Add(Convert.ToString(List_of_products.SelectedItem));
                        nameProduct = Convert.ToString(List_of_products.SelectedItem).Split('.')[1].Split('-')[0].Trim();
                        idProductForCount = Convert.ToInt32(Convert.ToString(List_of_products.SelectedItem).Split(' ', '.')[1]);
                        price = Convert.ToDouble(Convert.ToString(List_of_products.SelectedItem).Split('(')[1].Split('₽')[0]);
                        Count count = new Count();
                        count.Show();
                        Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
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
            foreach (string i in MainWindow.Products)
            {
                if (i.Contains(Search_product.Text.Trim()) || i.ToLower().Contains(Search_product.Text.Trim()))
                {
                    List_of_products.Items.Add(i);
                }
            }
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
