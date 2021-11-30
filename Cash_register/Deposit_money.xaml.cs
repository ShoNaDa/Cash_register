using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Deposit_money.xaml
    /// </summary>
    public partial class Deposit_money : Window
    {
        List<string> Signs = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", ".", "," };
        public Deposit_money()
        {
            InitializeComponent();
            //с этой штукой правильно работает точка и запятая
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        }

        private void Click_go(object sender, RoutedEventArgs e)
        {
            //проверяем нет ли пустых полей
            if (depositMoneyCount.Text != "")
            {
                bool depositCountIsOk = false;
                //проверяем правильно ли написана сумма внесения
                for (int i = 0; i < depositMoneyCount.Text.Length; i++)
                {
                    if (depositCountIsOk)
                    {
                        depositCountIsOk = false;
                    }
                    for (int j = 0; j < Signs.Count; j++)
                    {
                        if (Convert.ToString(depositMoneyCount.Text[i]).Contains(Signs[j]))
                        {
                            depositCountIsOk = true;
                            break;
                        }
                    }
                    if (depositCountIsOk == false)
                    {
                        break;
                    }
                }
                int count = 0;
                for (int i = 0; i < depositMoneyCount.Text.Length; i++)
                {
                    if (Convert.ToString(depositMoneyCount.Text[i]) == "." || Convert.ToString(depositMoneyCount.Text[i]) == ",")
                    {
                        count++;
                        if (count == 2)
                        {
                            depositCountIsOk = false;
                            break;
                        }
                    }
                }
                //если все ок
                if (depositCountIsOk && Convert.ToString(depositMoneyCount.Text[0]) != "." && Convert.ToString(depositMoneyCount.Text[0]) != "," &&
                    Convert.ToString(depositMoneyCount.Text[depositMoneyCount.Text.Length - 1]) != "." && Convert.ToString(depositMoneyCount.Text[depositMoneyCount.Text.Length - 1]) != ",")
                {
                    //проверяем: сумма внесения больше нуля?
                    if (Convert.ToDouble(depositMoneyCount.Text) > 0)
                    {
                        DataTable dt_deposit = Insert("Update Statements set Deposits = Deposits + " + Convert.ToDouble(Convert.ToString(depositMoneyCount.Text).Replace(',', '.')) + " where StatementsId = (select count(StatementsId) from Statements)");
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
