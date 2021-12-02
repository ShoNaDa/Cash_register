using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для StatementOfMonth.xaml
    /// </summary>
    public partial class StatementOfMonth : Window
    {
        public StatementOfMonth()
        {
            InitializeComponent();

            string month;
            DateTime date1 = DateTime.Now;
            if (Convert.ToString(date1.ToShortDateString()).Split('/')[0].Length == 1)
            {
                month = "0" + Convert.ToString(date1.ToShortDateString()).Split('/')[0];
            }
            else
            {
                month = Convert.ToString(date1.ToShortDateString()).Split('/')[0];
            }

            DataTable dt_shiftNumber = Select("Select ShiftNumber from Statements where Month(WorkingDate) = " + month);
            DataTable dt_FK_Worker = Select("Select FK_Worker from Statements where Month(WorkingDate) = " + month);
            DataTable dt_MoneyAtTheBeginningOfTheShift = Select("Select MoneyAtTheBeginningOfTheShift from Statements where Month(WorkingDate) = " + month);
            DataTable dt_Sales = Select("Select Sales from Statements where Month(WorkingDate) = " + month);
            DataTable dt_Refund = Select("Select Refund from Statements where Month(WorkingDate) = " + month);
            DataTable dt_Deposits = Select("Select Deposits from Statements where Month(WorkingDate) = " + month);
            DataTable dt_Withdrawals = Select("Select Withdrawals from Statements where Month(WorkingDate) = " + month);
            DataTable dt_WorkingDate = Select("Select WorkingDate from Statements where Month(WorkingDate) = " + month);

            for (int i = 0; i < dt_shiftNumber.Rows.Count; i++)
            {
                DataTable dt_FIO_Worker = Select("Select LName, FName, MName, RoleWorker from Workers where WorkersId = " + dt_FK_Worker.Rows[i][0]);
                statementOfMonth.Text += "\t\t\t  Смена №" + Convert.ToString(dt_shiftNumber.Rows[i][0] + "\n" +
                    "  " + Convert.ToString(dt_FIO_Worker.Rows[0][0]) + " " + Convert.ToString(dt_FIO_Worker.Rows[0][1]).Substring(0, 1) +
                    "." + Convert.ToString(dt_FIO_Worker.Rows[0][2]).Substring(0, 1) + ". — " + Convert.ToString(dt_FIO_Worker.Rows[0][3]) + "\n" +
                    "  Денег в начале смены: " + Convert.ToString(dt_MoneyAtTheBeginningOfTheShift.Rows[i][0]) + "\n" +
                    "  Продажи: " + Convert.ToString(dt_Sales.Rows[i][0]) + "\n" + 
                    "  Возвраты: " + Convert.ToString(dt_Refund.Rows[i][0]) + "\n" + 
                    "  Внесения: " + Convert.ToString(dt_Deposits.Rows[i][0]) + "\n" + 
                    "  Изъятия: " + Convert.ToString(dt_Withdrawals.Rows[i][0]) + "\n" +
                    "  Дата: " + Convert.ToString(dt_WorkingDate.Rows[i][0]).Split(' ')[0] + "\n" +
                    "\t ИТОГ: " + Convert.ToString(Convert.ToDouble(dt_MoneyAtTheBeginningOfTheShift.Rows[i][0]) + 
                    Convert.ToDouble(dt_Sales.Rows[i][0]) - Convert.ToDouble(dt_Refund.Rows[i][0]) +
                    Convert.ToDouble(dt_Deposits.Rows[i][0]) - Convert.ToDouble(dt_Withdrawals.Rows[i][0])) + "\n" +
                    "---------------------------------------------------------------------------" + "\n");
            }
        }

        private void Click_back(object sender, RoutedEventArgs e)
        {
            Statements statements = new Statements();
            statements.Show();
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

        private void Click_to_save(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовый документ (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (saveFileDialog.ShowDialog() != DialogResult.HasValue)
            {
                StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName);
                streamWriter.WriteLine(statementOfMonth.Text);
                streamWriter.Close();
            }
        }
    }
}
