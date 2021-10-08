using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для List_of_cashiers.xaml
    /// </summary>
    public partial class List_of_cashiers : Window
    {
        public List_of_cashiers()
        {
            InitializeComponent();

            DataTable dt_user = Select("SELECT * FROM [dbo].[Workers]");
            for (int i = 0; i < dt_user.Rows.Count; i++)
            {
                MainWindow.Cashiers.Add((string)dt_user.Rows[i][1] + " " + (string)dt_user.Rows[i][2] + " " + (string)dt_user.Rows[i][3]);
            }

            foreach (string i in MainWindow.Cashiers)
            {
                List_of_cashier.Items.Add(i);
            }

            MainWindow.FIO_cashier = (string)List_of_cashier.SelectedValue;
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

        private void Click_to_next(object sender, RoutedEventArgs e)
        {
            MainWindow.Cashiers.Clear();
            if (List_of_cashier.SelectedIndex != -1)
            {
                Authorization Autorization = new Authorization();
                Autorization.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Выберите кассира");
            }
        }

        private void Click_back(object sender, RoutedEventArgs e)
        {
            MainWindow.Cashiers.Clear();
            MainWindow Cash_register = new MainWindow();
            Cash_register.Show();
            Close();
        }
    }
}
