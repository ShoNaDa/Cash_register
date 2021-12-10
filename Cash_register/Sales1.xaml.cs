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
    /// Логика взаимодействия для Sales1.xaml
    /// </summary>
    public partial class Sales1 : Window
    {
        //List
        public static List<string> ProductsSale = new List<string>(1);
        public static List<string> SalesList = new List<string>(1);
        public static List<string> cheque = new List<string>();
        //double
        public double result;
        public static double fullPrice;

        public Sales1()
        {
            InitializeComponent();

            //с этой штукой правильно работает точка и запятая
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            foreach (string i in ProductsSale)
            {
                List_of_product_sale.Items.Add(i);
            }
            
            //если выбраны товары на продажу
            if (List_of_product_sale.Items.Count != 0)
            {
                //меняем цвет кнопки
                ToPay.Opacity = 1;
                toPay.Opacity = 1;
                Result.Opacity = 1;

                Result.Text = "";

                //формируем лист для тестов
                List<string> listProductsSale = new List<string>();
                foreach (string item in List_of_product_sale.Items)
                {
                    listProductsSale.Add(item);
                }

                //суммируется цена всех товаров
                Result.Text = Convert.ToString(CalculatingAmount(result, listProductsSale) + " ₽").Replace(',', '.');
            }
            else
            {
                //меняем цвет кнопки
                ToPay.Opacity = 0.5;
                toPay.Opacity = 0.5;
                Result.Opacity = 0.5;
            }
        }

        public void Click_to_be_paid(object sender, RoutedEventArgs e)
        {
            if (List_of_product_sale.Items.Count != 0)
            {
                foreach (string item in List_of_product_sale.Items)
                {
                    cheque.Add(item);
                }

                //узнали способ оплаты
                TipOfPament tipOfPament = new TipOfPament();
                tipOfPament.ShowDialog();

                //узнали ID смены
                DataTable dt_ShiftId = SQLrequest("Select ShiftId from [Shift] where ShiftId = (select max(ShiftId) from [Shift])");
                int shiftId = Convert.ToInt32(dt_ShiftId.Rows[0][0]);

                //узнали ID оплаты
                DataTable dt_PamentId = SQLrequest("Select PamentId from Pament where PamentId = (select max(PamentId) from Pament)");
                int pamentId = Convert.ToInt32(dt_PamentId.Rows[0][0]);

                //добавляем продажу
                SQLrequest("Insert into Sale values (" + shiftId + ", " + pamentId + ", 0)");

                //ID этой продажи
                DataTable dt_SaleId = SQLrequest("Select SaleId from Sale where SaleId = (select max(SaleId) from Sale)");
                int saleId = Convert.ToInt32(dt_SaleId.Rows[0][0]);

                //изменяем количество у проданных товаров
                for (int i = 0; i < List_of_product_sale.Items.Count; i++)
                {
                    //проданное количество и ID товара
                    string count = Convert.ToString(List_of_product_sale.Items[i]).Split('(')[1].Split(' ')[0].Trim();
                    string id = Convert.ToString(List_of_product_sale.Items[i]).Split(':')[1].Split('.')[0].Trim();
                    
                    //убираем проданное количество товара со склада
                    SQLrequest("Update Products set ProductCount = ProductCount - " + count + " where ProductId = " + id);
                    
                    //Добавляем инфу в БД про проданные товары
                    SQLrequest("Insert into ProductsList values (" + id + ", " + saleId + ", " + count + ")");
                }

                //изменяем сумму продажи
                SQLrequest("Update Sale set Amount = Amount + " + Result.Text.Trim().Split('₽')[0] + " where SaleId = (select max(SaleId) from Sale)");

                fullPrice = Convert.ToDouble(Result.Text.Trim().Split('₽')[0]);

                ProductsSale.Clear();

                Cheque chequeWindow = new Cheque();
                chequeWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Товары не выбраны");
            }
        }

        //функция для подсчета суммы товаров
        public static double CalculatingAmount(double result, List<string> ListProductSale)
        {
            for (int i = 0; i < ListProductSale.Count; i++)
            {
                result += Convert.ToDouble(ListProductSale[i].Split('₽')[0].Split(':')[2].Trim());
            }

            return result;
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

            if (MainWindow.isCashier)
            {
                After_login_in_cashier window9 = new After_login_in_cashier();
                window9.Show();
                Close();
            }
            else 
            {
                After_logging_in window2 = new After_logging_in();
                window2.Show();
                Close();
            }
        }

        private void Window3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Button_back.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            if (e.Key == Key.Enter)
            {
                Button_to_be_paid.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
    }
}