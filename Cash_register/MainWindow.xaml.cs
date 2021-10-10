using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //List
        public static List<string> Workers = new List<string>(1);
        public static List<string> Cashiers = new List<string>(1);
        public static List<string> Admins = new List<string>(1);
        //bool
        public static bool IsDeposit;
        public static bool IsSearchProducts = true;
        public static bool IsCashier;
        public static bool IsAdmin;
        //string
        public static string FIO_cashier;
        public MainWindow()
        {
            InitializeComponent();

            DataTable dt_admins = Select("SELECT * FROM [dbo].[Workers] WHERE roleWorker = 'Администратор'");
            for (int i = 0; i < dt_admins.Rows.Count; i++)
            {
                Admins.Add((string)dt_admins.Rows[i][1] + " " + (string)dt_admins.Rows[i][2] + " " + (string)dt_admins.Rows[i][3]);
            }
            DataTable dt_cashiers = Select("SELECT * FROM [dbo].[Workers] where roleWorker = 'Кассир'");
            for (int i = 0; i < dt_cashiers.Rows.Count; i++)
            {
                Cashiers.Add((string)dt_cashiers.Rows[i][1] + " " + (string)dt_cashiers.Rows[i][2] + " " + (string)dt_cashiers.Rows[i][3]);
            }
        }

        public void Click_to_cashier(object sender, RoutedEventArgs e)
        {
            IsAdmin = false;
            List_of_cashiers window11 = new List_of_cashiers();
            window11.Show();
            Close();
        }

        public void Click_to_admin(object sender, RoutedEventArgs e)
        {
            IsAdmin = true;
            List_of_admin window12 = new List_of_admin();
            window12.Show();
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
