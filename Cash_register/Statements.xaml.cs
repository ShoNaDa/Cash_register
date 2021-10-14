using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

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
            if (edit_time.dateAndTime != null)
            {
                time.Text = edit_time.dateAndTime;
            }
            //смена №...
            DataTable dt_shiftNumber = Select("SELECT count(ShifNumber) FROM Statements");
            MainWindow.shiftNumber = Convert.ToInt32(dt_shiftNumber);
            shiftNumber.Text = "Смена №" + MainWindow.shiftNumber;
            //... рублей в кассе
            DataTable dt_moneyInTheCashRegister = Select("SELECT MoneyInTheCashRegister FROM Statements where StatementsId = (select count(StatementsId) from Statements)");
            MainWindow.moneyInTheCashRegister = Convert.ToInt32(dt_moneyInTheCashRegister); ;
            moneyInTheCashRegister.Text = MainWindow.moneyInTheCashRegister + " ₽ в кассе";
            //денег в начале смены
            DataTable dt_moneyAtTheBeginningOfTheShift = Select("SELECT MoneyAtTheBeginningOfTheShift FROM Statements where StatementsId = (select count(StatementsId) from Statements)");
            MainWindow.moneyAtTheBeginningOfTheShift = Convert.ToInt32(dt_moneyAtTheBeginningOfTheShift); ;
            moneyAtTheBeginningOfTheShift.Text = Convert.ToString(MainWindow.moneyAtTheBeginningOfTheShift);
            //продажи
            DataTable dt_sales = Select("SELECT Sales FROM Statements where StatementsId = (select count(StatementsId) from Statements)");
            MainWindow.sales = Convert.ToInt32(dt_sales); ;
            sales.Text = Convert.ToString(MainWindow.sales);
            //возвраты
            DataTable dt_refund = Select("SELECT Refund FROM Statements where StatementsId = (select count(StatementsId) from Statements)");
            MainWindow.refund = Convert.ToInt32(dt_refund); ;
            refund.Text = Convert.ToString(MainWindow.refund);
            //изъятия
            DataTable dt_withdrawals = Select("SELECT Withdrawals FROM Statements where StatementsId = (select count(StatementsId) from Statements)");
            MainWindow.withdrawals = Convert.ToInt32(dt_withdrawals); ;
            withdrawals.Text = Convert.ToString(MainWindow.withdrawals);
            //внесения
            DataTable dt_deposits = Select("SELECT Deposits FROM Statements where StatementsId = (select count(StatementsId) from Statements)");
            MainWindow.deposits = Convert.ToInt32(dt_deposits); ;
            deposits.Text = Convert.ToString(MainWindow.deposits);
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
            
        }

        public void Click_to_withdraw(object sender, RoutedEventArgs e)
        {
            
        }

        public void Click_to_close_shift(object sender, RoutedEventArgs e)
        {

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
