﻿using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Workers.xaml
    /// </summary>
    public partial class Workers : Window
    {
        public Workers()
        {
            InitializeComponent();

            DataTable dt_workers = Select("SELECT * FROM [dbo].[Workers]");
            for (int i = 0; i < dt_workers.Rows.Count; i++)
            {
                MainWindow.Workers.Add((string)dt_workers.Rows[i][1] + " " + ((string)dt_workers.Rows[i][2]).Substring(0, 1) + ". " + ((string)dt_workers.Rows[i][3]).Substring(0, 1) + ". (" + (string)dt_workers.Rows[i][4] + ")");
            }

            foreach (string i in MainWindow.Workers)
            {
                List_of_workers.Items.Add(i);
            }
        }

        private void Click_back(object sender, RoutedEventArgs e)
        {
            MainWindow.Workers.Clear();
            Settings window7 = new Settings();
            window7.Show();
            Close();
        }

        private void Click_to_add_worker(object sender, RoutedEventArgs e)
        {
            MainWindow.Workers.Clear();
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

        private void Click_to_edit_worker(object sender, RoutedEventArgs e)
        {
            MainWindow.Workers.Clear();
            if (List_of_workers.SelectedIndex != -1)
            {
                
            }
            else
            {
                MessageBox.Show("Выберите сотрудника");
            }
        }
    }
}