using System;
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
        //List
        List<string> Numbers = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        static List<string> ListOfProductsSold = new List<string>(1);
        static List<int> numberOfChequeProductSold = new List<int>();
        //Dictionary
        public static Dictionary<int, double> refunds = new Dictionary<int, double>();
        //double
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
                double amount = 0;
                amount = AmountOfSoldProducts(amount, Convert.ToDouble(dt_productsInfo.Rows[0][1]), Convert.ToDouble(dt_count.Rows[0][0]));

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
                        + " (Цена: " + Convert.ToString(Convert.ToDouble(dt_productsInfo.Rows[0][1])) + "₽). Количество: " + Convert.ToString(dt_count.Rows[0][0]) + ", Сумма: " + Convert.ToString(amount));
                }
            }

            for (int i = 0; i < List_of_products_sold.Items.Count; i++)
            {
                ListOfProductsSold.Add(Convert.ToString(List_of_products_sold.Items[i]));
            }
        }

        public void Click_back(object sender, RoutedEventArgs e)
        {
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

        public void Click_to_find_a_cheque(object sender, RoutedEventArgs e)
        {
            //делаем видимым текстблок для поиска
            Button_find_a_cheque.Visibility = Visibility.Hidden;
            Search_cheque.Visibility = Visibility.Visible;
            Button_searching.Visibility = Visibility.Visible;

            Search_cheque.Focus();
        }

        private void Click_searching(object sender, RoutedEventArgs e)
        {
            bool isOk = false;

            List_of_products_sold.Items.Clear();

            //поиск, если чек - цифра
            if (ChequeIsValid(Search_cheque.Text, isOk, Numbers))
            {
                //создаем новый лист для записи
                List<string> NewListSoldProducts = new List<string>();

                //заполняем новый список
                ListOfProductSold(ListOfProductsSold.Count, Search_cheque.Text, ListOfProductsSold, NewListSoldProducts);

                //заполняем данными окно по поиску
                foreach(string item in NewListSoldProducts)
                {
                    List_of_products_sold.Items.Add(item);
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

                //возвращаем количество товара на склад
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

                //выводим каким способом этот товар оплачивали
                DataTable dt_tipPament = SQLrequest("Select TipOfPament from Pament where PamentId = (select FK_SaleId from ProductsList where ProductsListId = " + idInList + ")" );
                if (Convert.ToString(dt_tipPament.Rows[0][0]) == "Наличные")
                {
                    MessageBox.Show("Возврат наличными");
                }
                else
                {
                    MessageBox.Show("Возврат на счет банковской карты");
                }

                Refund_of_products window5 = new Refund_of_products();
                window5.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Выберите товар");
            }
        }

        //функция проверяет, что чек написан цифрой
        public static bool ChequeIsValid(string searchCheque, bool isOk, List<string> Numbers)
        {
            for (int i = 0; i < searchCheque.Length; i++)
            {
                if (isOk)
                {
                    isOk = false;
                }
                for (int j = 0; j < Numbers.Count; j++)
                {
                    if (Convert.ToString(searchCheque[i]).Contains(Numbers[j]))
                    {
                        isOk = true;
                        break;
                    }
                }
                if (!isOk)
                {
                    break;
                }
            }

            return isOk;
        }

        //функция поиска валидного чека
        public static List<string> ListOfProductSold(int count, string searchCheque, List<string> ListSoldProducts, List<string> NewListSoldProducts)
        {
            for (int i = 0; i < count; i++)
            {
                if (Convert.ToString(ListSoldProducts[i]).Split(':')[1].Split('.')[0].Trim().Contains(searchCheque.Trim()))
                {
                    NewListSoldProducts.Add(ListSoldProducts[i]);
                }
            }

            return NewListSoldProducts;
        }

        //функция высчитывает сумма проданных товаров
        public static double AmountOfSoldProducts(double amount, double sale, double count)
        {
            amount = sale * count;

            return amount;
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