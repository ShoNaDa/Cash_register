using System;
using System.Data;
using static Cash_register.SQLRequest;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls.Primitives;

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
        public static int productCount;

        public object Button_open { get; private set; }

        public Product_search()
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
            After_logging_in window2 = new After_logging_in();
            window2.Show();
            Close();
        }

        public void Click_to_find_a_product(object sender, RoutedEventArgs e)
        {
            Button_find_a_product.Visibility = Visibility.Hidden;
            Search_product.Visibility = Visibility.Visible;
            Button_search.Visibility = Visibility.Visible;

            Search_product.Focus();
        }
        private void Click_to_drop_product(object sender, RoutedEventArgs e)
        {
            if (List_of_products.SelectedIndex != -1)
            {
                int id = Convert.ToInt32(Convert.ToString(List_of_products.SelectedItem).Split(' ', '.')[1]);
                SQLrequest("delete from [dbo].[Products] WHERE ProductId = " + id);
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
                nameProduct = Convert.ToString(List_of_products.SelectedItem).Split('.')[1].Split('-')[0].Trim();
                idProduct = Convert.ToInt32(Convert.ToString(List_of_products.SelectedItem).Split(' ', '.')[1]);
                salePrice = Convert.ToDouble(Convert.ToString(List_of_products.SelectedItem).Split('(')[1].Split('₽')[0]);
                productCount = Convert.ToInt32(Convert.ToString(List_of_products.SelectedItem).Split('-')[1].Split('ш')[0].Trim());
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

        private void window4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Button_back.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            if (e.Key == Key.Delete)
            {
                Click_to_drop_product(sender, e);
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
