using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Withdrawals_money.xaml
    /// </summary>
    public partial class Withdrawals_money : Window
    {
        List<string> Signs = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", ".", "," };
        public Withdrawals_money()
        {
            InitializeComponent();
            //с этой штукой правильно работает точка и запятая
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        }

        private void Click_goo(object sender, RoutedEventArgs e)
        {
            //проверяем нет ли пустых полей
            if (withdrawalsMoneyCount.Text != "")
            {
                bool withdrawalsCountIsOk = false;
                //проверяем правильно ли написана сумма внесения
                for (int i = 0; i < withdrawalsMoneyCount.Text.Length; i++)
                {
                    if (withdrawalsCountIsOk)
                    {
                        withdrawalsCountIsOk = false;
                    }
                    for (int j = 0; j < Signs.Count; j++)
                    {
                        if (Convert.ToString(withdrawalsMoneyCount.Text[i]).Contains(Signs[j]))
                        {
                            withdrawalsCountIsOk = true;
                            break;
                        }
                    }
                    if (withdrawalsCountIsOk == false)
                    {
                        break;
                    }
                }
                int count = 0;
                for (int i = 0; i < withdrawalsMoneyCount.Text.Length; i++)
                {
                    if (Convert.ToString(withdrawalsMoneyCount.Text[i]) == "." || Convert.ToString(withdrawalsMoneyCount.Text[i]) == ",")
                    {
                        count++;
                        if (count == 2)
                        {
                            withdrawalsCountIsOk = false;
                            break;
                        }
                    }
                }
                //если все ок
                if (withdrawalsCountIsOk && Convert.ToString(withdrawalsMoneyCount.Text[0]) != "." && Convert.ToString(withdrawalsMoneyCount.Text[0]) != "," &&
                    Convert.ToString(withdrawalsMoneyCount.Text[withdrawalsMoneyCount.Text.Length - 1]) != "." && Convert.ToString(withdrawalsMoneyCount.Text[withdrawalsMoneyCount.Text.Length - 1]) != ",")
                {
                    //проверяем: сумма изъятия больше нуля?
                    if (Convert.ToDouble(withdrawalsMoneyCount.Text) > 0)
                    {
                        DataTable dt = Insert("select MoneyInTheCashRegister from Statements where StatementsId = (select count(StatementsId) from Statements)");
                        if (Convert.ToDouble(Convert.ToString(dt.Rows[0][0]).Replace('.', ',')) >= Convert.ToDouble(withdrawalsMoneyCount.Text))
                        {
                            DataTable dt_withdrawals = Insert("Update Statements set Withdrawals = Withdrawals + " + Convert.ToDouble(Convert.ToString(withdrawalsMoneyCount.Text).Replace(',', '.')) + " where StatementsId = (select count(StatementsId) from Statements)");
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
                    MessageBox.Show("Неправильный формат");
                }
            }
            else
            {
                MessageBox.Show("Все строки должны быть заполнены");
            }
        }

        public DataTable Insert(string selectSQL) // функция подключения к базе данных и обработка запросов
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

        private void Click_back(object sender, RoutedEventArgs e)
        {
            Statements statements = new Statements();
            statements.Show();
            Close();
        }
    }
}