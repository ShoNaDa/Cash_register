﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Statements.xaml
    /// </summary>
    public partial class Statements : Window
    {
        //функция для установки времени
        public void Date()
        {
            DateTime date1 = DateTime.Now;
            if (edit_time.dateAndTime != 0)
            {
                switch (edit_time.dateAndTime)
                {
                    case 1:
                        time.Text = Convert.ToString(date1.ToLongDateString());
                        break;
                    case 2:
                        time.Text = Convert.ToString(date1.ToShortDateString());
                        break;
                    case 3:
                        time.Text = Convert.ToString(date1.ToLongTimeString());
                        break;
                    case 4:
                        time.Text = Convert.ToString(date1.ToShortTimeString());
                        break;
                    case 5:
                        time.Text = Convert.ToString(date1.ToLocalTime());
                        break;
                }
            }
            else
            {
                time.Text = Convert.ToString(date1);
            }
        }
        public Statements()
        {
            InitializeComponent();

            //с этой штукой правильно работает точка и запятая
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            //смена №...
            DataTable dt_shiftNumber = Select("SELECT count(ShiftNumber) FROM Statements");
            MainWindow.shiftNumber = Convert.ToInt32(dt_shiftNumber.Rows[0][0]);
            shiftNumber.Text = "Смена №" + MainWindow.shiftNumber;
            //денег в начале смены
            DataTable dt_moneyAtTheBeginningOfTheShift = Select("SELECT MoneyAtTheBeginningOfTheShift FROM Statements where StatementsId = (select count(StatementsId) from Statements)");
            MainWindow.moneyAtTheBeginningOfTheShift = Convert.ToDouble(dt_moneyAtTheBeginningOfTheShift.Rows[dt_moneyAtTheBeginningOfTheShift.Rows.Count - 1][0]); ;
            moneyAtTheBeginningOfTheShift.Text = Convert.ToString(MainWindow.moneyAtTheBeginningOfTheShift);
            //продажи
            DataTable dt_sales = Select("SELECT Sales FROM Statements where StatementsId = (select count(StatementsId) from Statements)");
            MainWindow.sales = Convert.ToDouble(dt_sales.Rows[dt_sales.Rows.Count - 1][0]); ;
            sales.Text = Convert.ToString(MainWindow.sales);
            //возвраты
            DataTable dt_refund = Select("SELECT Refund FROM Statements where StatementsId = (select count(StatementsId) from Statements)");
            MainWindow.refund = Convert.ToDouble(dt_refund.Rows[dt_refund.Rows.Count - 1][0]); ;
            refund.Text = Convert.ToString(MainWindow.refund);
            //изъятия
            DataTable dt_withdrawals = Select("SELECT Withdrawals FROM Statements where StatementsId = (select count(StatementsId) from Statements)");
            MainWindow.withdrawals = Convert.ToDouble(dt_withdrawals.Rows[dt_withdrawals.Rows.Count - 1][0]); ;
            withdrawals.Text = Convert.ToString(MainWindow.withdrawals);
            //внесения
            DataTable dt_deposits = Select("SELECT Deposits FROM Statements where StatementsId = (select count(StatementsId) from Statements)");
            MainWindow.deposits = Convert.ToDouble(dt_deposits.Rows[dt_deposits.Rows.Count - 1][0]); ;
            deposits.Text = Convert.ToString(MainWindow.deposits);
            //... рублей в кассе
            DataTable MoneyInTheCashRegister = Select("Update Statements set MoneyInTheCashRegister = " + Convert.ToDouble(dt_moneyAtTheBeginningOfTheShift.Rows[0][0]) + " + " + Convert.ToDouble(dt_sales.Rows[0][0]) + " - " + Convert.ToDouble(dt_refund.Rows[0][0]) + " - " + Convert.ToDouble(dt_withdrawals.Rows[0][0]) + " + " + Convert.ToDouble(dt_deposits.Rows[0][0]) + " where StatementsId = (select count(StatementsId) from Statements)");
            DataTable dt_moneyInTheCashRegister = Select("SELECT MoneyInTheCashRegister FROM Statements where StatementsId = (select count(StatementsId) from Statements)");
            MainWindow.moneyInTheCashRegister = Convert.ToDouble(dt_moneyInTheCashRegister.Rows[dt_moneyInTheCashRegister.Rows.Count - 1][0]); ;
            moneyInTheCashRegister.Text = MainWindow.moneyInTheCashRegister + "₽ в кассе";
        }
        //событие для обновления времени на текущее при перемещении курсора мыши
        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            Date();
        }
        public void Click_back(object sender, RoutedEventArgs e)
        {
            if (MainWindow.IsCashier == true)
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

        public DataTable Select(string selectSQL) // функция подключения к базе данных и обработка запросов
        {
            DataTable dataTable = new DataTable("dataBase");                // создаём таблицу в приложении
                                                                            // подключаемся к базе данных
            SqlConnection sqlConnection = new SqlConnection(@"server=WIN-PA0KKAO063F\SQLEXPRESS;Trusted_Connection=Yes;DataBase=Cash_register;");
            sqlConnection.Open();                                           // открываем базу данных
            SqlCommand sqlCommand = sqlConnection.CreateCommand();          // создаём команду
            sqlCommand.CommandText = selectSQL;                             // присваиваем команде текст
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); // создаём обработчик
            sqlDataAdapter.Fill(dataTable);                                 // возращаем таблицу с результатом
            sqlConnection.Close();
            return dataTable;
        }
    }
}
