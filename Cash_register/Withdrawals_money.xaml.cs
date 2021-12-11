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
    /// Логика взаимодействия для Withdrawals_money.xaml
    /// </summary>
    public partial class Withdrawals_money : Window
    {
        //List
        private readonly List<string> Signs = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", ".", "," };
        
        public Withdrawals_money()
        {
            InitializeComponent();

            //с этой штукой правильно работает точка и запятая
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            withdrawalsMoneyCount.Focus();
        }

        private void Click_goo(object sender, RoutedEventArgs e)
        {
            //проверяем нет ли пустых полей
            if (withdrawalsMoneyCount.Text != "")
            {
                bool withdrawalsсountIsOk = false;

                if (ValidIsOk(withdrawalsMoneyCount.Text, withdrawalsсountIsOk, Signs))
                {
                    withdrawalsсountIsOk = true;
                }

                //если все ок
                if (PriceValid(withdrawalsMoneyCount.Text, withdrawalsсountIsOk) &&
                    Convert.ToDouble(withdrawalsMoneyCount.Text) > 0)
                {
                    //проверка - в кассе есть такая сумма?
                    DataTable dt = SQLrequest("Select MoneyInTheCashRegister from BalanceAfterCloseCashRegister where BalanceId = (select max(BalanceId) from BalanceAfterCloseCashRegister)");

                    if (Convert.ToDouble(Convert.ToString(dt.Rows[0][0])) >= Convert.ToDouble(withdrawalsMoneyCount.Text))
                    {
                        SQLrequest("Update BalanceAfterCloseCashRegister set Withdrawals = Withdrawals + " + Convert.ToDouble(Convert.ToString(withdrawalsMoneyCount.Text).Replace(',', '.')) + " where BalanceId = (select max(BalanceId) from BalanceAfterCloseCashRegister)");
                        
                        Statements statements = new Statements();
                        statements.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("В кассе нет столько денег");
                    }
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
        private void Click_back(object sender, RoutedEventArgs e)
        {
            Statements statements = new Statements();
            statements.Show();
            Close();
        }

        private void WithdrawalsMoney_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Button_back.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            if (e.Key == Key.Enter)
            {
                Button_goo.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
    }
}