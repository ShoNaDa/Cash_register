﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Close_shift.xaml
    /// </summary>
    public partial class Close_shift : Window
    {
        public Close_shift()
        {
            InitializeComponent();
        }

        private void Click_back(object sender, RoutedEventArgs e)
        {
            Statements statements = new Statements();
            statements.Show();
            Close();
        }

        private void Click_close(object sender, RoutedEventArgs e)
        {
            DataTable dt_UpdateShiftFK_Worker = Select("Update Statements set FK_Worker = " + Authorization.id + "where StatementsId = (select count(*) from Statements)");
            DateTime date1 = DateTime.Now;
            string month, day;
            if (Convert.ToString(date1.ToShortDateString()).Split('/')[0].Length == 1)
            {
                month = "0" + Convert.ToString(date1.ToShortDateString()).Split('/')[0];
            }
            else
            {
                month = Convert.ToString(date1.ToShortDateString()).Split('/')[0];
            }
            if (Convert.ToString(date1.ToShortDateString()).Split('/')[1].Length == 1)
            {
                day = "0" + Convert.ToString(date1.ToShortDateString()).Split('/')[1];
            }
            else
            {
                day = Convert.ToString(date1.ToShortDateString()).Split('/')[1];
            }
            DataTable dt_UpdateShiftWorkingDate = Select("Update Statements set WorkingDate = '" + Convert.ToString(date1.ToShortDateString()).Split('/')[2] + month + day + "' where StatementsId = (select count(*) from Statements)");            
            DataTable dt_newShift = Select("Insert into Statements values ((select count(ShiftNumber) from Statements) + 1, " + MainWindow.moneyInTheCashRegister + ", " + MainWindow.moneyInTheCashRegister + ", 0, 0, 0, 0, 1, '" + Convert.ToString(date1.ToShortDateString()).Split('/')[2] + month + day + "')");
            Open_shift openShift = new Open_shift();
            openShift.Show();
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
