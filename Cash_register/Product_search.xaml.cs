using System;
using System.Data;
using static Cash_register.SQLRequest;
using static Cash_register.SearchFunc;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using System.Collections.Generic;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Product_search.xaml
    /// </summary>
    public partial class Product_search : Window
    {
        //string
        public static string nameProduct;
        //int
        public static int idProduct;
        public static int productCount;
        //double
        public static double salePrice;

        public Product_search()
        {
            InitializeComponent();

            //выводим список всех продуктов
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
            //при нажатии на поиск появляется токстбокс
            Button_find_a_product.Visibility = Visibility.Hidden;
            Search_product.Visibility = Visibility.Visible;
            Button_search.Visibility = Visibility.Visible;

            Search_product.Focus();
        }
        private void Click_to_drop_product(object sender, RoutedEventArgs e)
        {
            if (List_of_products.SelectedIndex != -1)
            {
                //ID товара, который хотим удалить
                int id = Convert.ToInt32(Convert.ToString(List_of_products.SelectedItem).Split(' ', '.')[1]);
                //удаление
                SQLrequest("Delete from [dbo].[Products] WHERE ProductId = " + id);
                
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
                //вытаскиваем название продукта
                nameProduct = Convert.ToString(List_of_products.SelectedItem).Split('.')[1].Split('-')[0].Trim();
                //его ID
                idProduct = Convert.ToInt32(Convert.ToString(List_of_products.SelectedItem).Split(' ', '.')[1]);
                //его цену
                salePrice = Convert.ToDouble(Convert.ToString(List_of_products.SelectedItem).Split('(')[1].Split('₽')[0]);
                //его количество на складе
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

            //создаем лист для заполнения искомыми товарами
            List<string> ListOfProduct = new List<string>();

            //искает...
            SearchProduct(MainWindow.Products, Search_product.Text, ListOfProduct);
            
            //выводим в окошко искомые товары
            foreach(string item in ListOfProduct)
            {
                List_of_products.Items.Add(item);
            }
        }

        private void Window4_KeyDown(object sender, KeyEventArgs e)
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
