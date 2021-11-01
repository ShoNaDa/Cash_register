using System;
using System.Collections.Generic;
using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Sales1.xaml
    /// </summary>
    public partial class Sales1 : Window
    {
        public static List<string> ProductsSale = new List<string>(1);
        public double result;
        public Sales1()
        {
            InitializeComponent();

            foreach (string i in ProductsSale)
            {
                List_of_product_sale.Items.Add(i);
            }
            
            if (List_of_product_sale.Items.Count != 0)
            {
                ToPay.Opacity = 1;
                for (int i = 0; i < List_of_product_sale.Items.Count; i++)
                {
                    Result.Text = "";
                    result += Convert.ToDouble(Convert.ToString(List_of_product_sale.Items[i]).Split('₽')[0].Split(':')[2].Trim());
                }
                Result.Text = Convert.ToString(result + " ₽");
            }
            else
            {
                ToPay.Opacity = 0.5;
            }
        }

        public void Click_to_be_paid(object sender, RoutedEventArgs e)
        {
            if (List_of_product_sale.Items.Count != 0)
            {
                MessageBox.Show("Оплачено");
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
            else
            {
                MessageBox.Show("Товары не выбраны");
            }
        }

        public void Click_add_product(object sender, RoutedEventArgs e)
        {
            Products_sale Product_sale = new Products_sale();
            Product_sale.Show();
            Close();
        }

        public void Click_back(object sender, RoutedEventArgs e)
        {
            ProductsSale.Clear();
            if (MainWindow.IsCashier)
            {
                After_login_in_cashier window9 = new After_login_in_cashier();
                window9.Show();
                Close();
            }
            else if(MainWindow.IsCashier == false)
            {
                After_logging_in window2 = new After_logging_in();
                window2.Show();
                Close();
            }
        }
    }
}
