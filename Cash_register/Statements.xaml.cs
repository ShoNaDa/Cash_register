using System;
using System.Data;
using static Cash_register.SQLRequest;
using static Cash_register.DateFunc;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls.Primitives;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Statements.xaml
    /// </summary>
    public partial class Statements : Window
    {
        public Statements()
        {
            InitializeComponent();

            //выводим дату
            time.Text = Date(edit_time.dateAndTime);

            //с этой штукой правильно работает точка и запятая
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            //ID смены
            DataTable dt_shiftId = SQLrequest("SELECT max(ShiftId) FROM [Shift]");
            
            //смена №...
            DataTable dt_shiftNumber = SQLrequest("SELECT max(ShiftNumber) FROM [Shift]");
            MainWindow.shiftNumber = Convert.ToInt32(dt_shiftNumber.Rows[0][0]);
            shiftNumber.Text = "Смена №" + MainWindow.shiftNumber;
            
            //денег в начале смены
            DataTable dt_moneyAtTheBeginningOfTheShift = SQLrequest("SELECT MoneyAtTheBeginningOfTheShift FROM BalanceAfterCloseCashRegister where BalanceId = " + dt_shiftId.Rows[0][0]);
            MainWindow.moneyAtTheBeginningOfTheShift = Convert.ToDouble(dt_moneyAtTheBeginningOfTheShift.Rows[dt_moneyAtTheBeginningOfTheShift.Rows.Count - 1][0]);
            moneyAtTheBeginningOfTheShift.Text = Convert.ToString(MainWindow.moneyAtTheBeginningOfTheShift);
            
            //продажи
            DataTable dt_sales = new DataTable(); ;
            
            try
            {
                dt_sales = SQLrequest("SELECT sum(Amount) FROM Sale where FK_ShiftId = " + dt_shiftId.Rows[0][0]);
                MainWindow.sales = Convert.ToDouble(dt_sales.Rows[dt_sales.Rows.Count - 1][0]); ;
                sales.Text = Convert.ToString(MainWindow.sales);
            }
            catch
            {
                sales.Text = "0";
            }
            
            //возвраты
            MainWindow.refund = Refund_of_products.refundOfShift; ;
            refund.Text = Convert.ToString(MainWindow.refund);
            
            //изъятия
            DataTable dt_withdrawals = SQLrequest("SELECT Withdrawals FROM BalanceAfterCloseCashRegister where BalanceId = " + dt_shiftId.Rows[0][0]);
            MainWindow.withdrawals = Convert.ToDouble(dt_withdrawals.Rows[dt_withdrawals.Rows.Count - 1][0]); ;
            withdrawals.Text = Convert.ToString(MainWindow.withdrawals);
            
            //внесения
            DataTable dt_deposits = SQLrequest("SELECT Deposits FROM BalanceAfterCloseCashRegister where BalanceId = " + dt_shiftId.Rows[0][0]);
            MainWindow.deposits = Convert.ToDouble(dt_deposits.Rows[dt_deposits.Rows.Count - 1][0]); ;
            deposits.Text = Convert.ToString(MainWindow.deposits);
            
            //... рублей в кассе
            try
            {
                SQLrequest("Update BalanceAfterCloseCashRegister set MoneyInTheCashRegister = " + Convert.ToDouble(dt_moneyAtTheBeginningOfTheShift.Rows[0][0]) + " + " + Convert.ToDouble(dt_sales.Rows[0][0]) + " " +
                "- " + Refund_of_products.refundOfShift + " - " + Convert.ToDouble(dt_withdrawals.Rows[0][0]) + " + " + Convert.ToDouble(dt_deposits.Rows[0][0]) + " where BalanceId = " + dt_shiftId.Rows[0][0]);
            }
            catch
            {
                SQLrequest("Update BalanceAfterCloseCashRegister set MoneyInTheCashRegister = " + Convert.ToDouble(dt_moneyAtTheBeginningOfTheShift.Rows[0][0]) + " + " + 0 + " " +
                "- " + Refund_of_products.refundOfShift + " - " + Convert.ToDouble(dt_withdrawals.Rows[0][0]) + " + " + Convert.ToDouble(dt_deposits.Rows[0][0]) + " where BalanceId = " + dt_shiftId.Rows[0][0]);
            }
            
            //Денег в кассе
            DataTable dt_moneyInTheCashRegister = SQLrequest("SELECT MoneyInTheCashRegister FROM BalanceAfterCloseCashRegister where BalanceId = " + dt_shiftId.Rows[0][0]);
            MainWindow.moneyInTheCashRegister = Convert.ToDouble(dt_moneyInTheCashRegister.Rows[dt_moneyInTheCashRegister.Rows.Count - 1][0]); ;
            moneyInTheCashRegister.Text = MainWindow.moneyInTheCashRegister + "₽ в кассе";

            //изменяем таблицу конца смены
            SQLrequest("Update [Shift] set CloseShiftDate = '" + DateFunction()[2] + DateFunction()[0] + DateFunction()[1] + "' where ShiftId = (select max(ShiftId) from [Shift])");
        }


        //событие для обновления времени на текущее при перемещении курсора мыши
        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            time.Text = Date(edit_time.dateAndTime);
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

        public void Click_to_deposit(object sender, RoutedEventArgs e)
        {
            Deposit_money depositMoney = new Deposit_money();
            depositMoney.Show();
            Close();
        }

        public void Click_to_withdraw(object sender, RoutedEventArgs e)
        {
            Withdrawals_money withdrawalsMoney = new Withdrawals_money();
            withdrawalsMoney.Show();
            Close();
        }

        public void Click_to_close_shift(object sender, RoutedEventArgs e)
        {
            Close_shift closeShift = new Close_shift();
            closeShift.Show();
            Close();
        }

        private void Click_to_StatementsOfMonth(object sender, RoutedEventArgs e)
        {
            StatementOfMonth statementOfMonth = new StatementOfMonth();
            statementOfMonth.Show();
            Close();
        }

        //функция для установки времени
        public static string Date(int count)
        {
            string text;
            DateTime date1 = DateTime.Now;

            switch (count)
            {
                case 1:
                    text = Convert.ToString(date1.ToLongDateString());
                    break;
                case 2:
                    text = Convert.ToString(date1.ToShortDateString());
                    break;
                case 3:
                    text = Convert.ToString(date1.ToLongTimeString());
                    break;
                case 4:
                    text = Convert.ToString(date1.ToShortTimeString());
                    break;
                case 5:
                    text = Convert.ToString(date1.ToLocalTime());
                    break;
                default:
                    text = Convert.ToString(date1);
                    break;
            }

            return text;
        }

        private void Window6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Button_back.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
    }
}