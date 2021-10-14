using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Add_worker.xaml
    /// </summary>
    public partial class Add_worker : Window
    {
        public Add_worker()
        {
            InitializeComponent();
        }

        private void Click_back(object sender, RoutedEventArgs e)
        {
            Workers window13 = new Workers();
            window13.Show();
            Close();
        }

        private void Click_to_add(object sender, RoutedEventArgs e)
        {
            if(add_lname.Text != "" && add_fname.Text != "" && add_mname.Text != "" && add_roleWorker.Text != "" && add_pincode.Password != string.Empty)
            {
                DataTable dt_worker = Insert("Insert into [dbo].[Workers] values " +
                                                                          "('" + add_lname.Text +
                                                                        "', '" + add_fname.Text +
                                                                        "', '" + add_mname.Text +
                                                                        "', '" + add_roleWorker.Text +
                                                                        "', '" + add_pincode.Password + "')");
                MessageBox.Show("Данные успешно добавлены");
                After_logging_in window2 = new After_logging_in();
                window2.Show();
                Close();
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
    }
}
