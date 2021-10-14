using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
            string fName = MainWindow.FIO_worker.Split(' ')[1];
            string lName = MainWindow.FIO_worker.Split(' ')[0];
            string mName = MainWindow.FIO_worker.Split(' ')[2];
            Worker.Text = string.Concat(lName, " ", fName.Substring(0, 1), ". ", mName.Substring(0, 1), ". ");
        }

        private void Click_back(object sender, RoutedEventArgs e)
        {
            MainWindow Cash_register = new MainWindow();
            Cash_register.Show();
            Close();
        }

        private void Click_to_enter(object sender, RoutedEventArgs e)
        {
            if (MainWindow.IsAdmin)
            {
                int counter = 0;
                int id = 0;
                foreach (int i in MainWindow.Workers_ID_admins)
                {
                    if (counter == List_of_admin.index)
                    {
                        id = i;
                    }
                    counter++; ;
                }

                DataTable dt_admins = Select("SELECT * FROM [dbo].[Workers] where roleWorker = 'Администратор' and WorkersId = " + id);
                for (int i = 0; i < dt_admins.Rows.Count; i++)
                {
                    if (password.Password == (string)dt_admins.Rows[i][5])
                    {
                        After_logging_in window2 = new After_logging_in();
                        window2.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Неправильный пароль");
                        break;
                    }
                }
            }
            else if (MainWindow.IsAdmin == false)
            {
                int counter = 0;
                int id = 0;
                foreach (int i in MainWindow.Workers_ID_cashiers)
                {
                    if (counter == List_of_cashiers.index)
                    {
                        id = i;
                    }
                    counter++; ;
                }

                DataTable dt_cashiers = Select("SELECT * FROM [dbo].[Workers] where WorkersId = " + id);
                for (int i = 0; i < dt_cashiers.Rows.Count; i++)
                {
                    if (password.Password == (string)dt_cashiers.Rows[i][5])
                    {
                        After_login_in_cashier window9 = new After_login_in_cashier();
                        window9.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Неправильный пароль");
                        break;
                    }
                }
            }
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
