using System;
using System.Collections.Generic;
using static Cash_register.SQLRequest;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using static Cash_register.ChekingValidityProduct;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Deposit_money.xaml
    /// </summary>
    public partial class Deposit_money : Window
    {
        //List
        private readonly List<string> Signs = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", ".", "," };

        public Deposit_money()
        {
            InitializeComponent();

            //с этой штукой правильно работает точка и запятая
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            depositMoneyCount.Focus();
        }

        private void Click_go(object sender, RoutedEventArgs e)
        {
            //проверяем нет ли пустых полей
            if (depositMoneyCount.Text != "")
            {
                bool depositсountIsOk = false;

                if (ValidIsOk(depositMoneyCount.Text, depositсountIsOk, Signs))
                {
                    depositсountIsOk = true;
                }

                //если все ок
                if (PriceValid(depositMoneyCount.Text, depositсountIsOk) &&
                    Convert.ToDouble(depositMoneyCount.Text) > 0)
                {
                    SQLrequest("Update BalanceAfterCloseCashRegister set Deposits = Deposits + " + Convert.ToDouble(Convert.ToString(depositMoneyCount.Text).Replace(',', '.')) + " where BalanceId = (select max(BalanceId) from BalanceAfterCloseCashRegister)");
                    
                    Statements statements = new Statements();
                    statements.Show();
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

        private void Click_back(object sender, RoutedEventArgs e)
        {
            Statements statements = new Statements();
            statements.Show();
            Close();
        }

        private void DepositMoney_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Button_back.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            if (e.Key == Key.Enter)
            {
                Button_go.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
    }
}
