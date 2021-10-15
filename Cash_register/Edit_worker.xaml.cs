using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Edit_worker.xaml
    /// </summary>
    public partial class Edit_worker : Window
    {
        public Edit_worker()
        {
            InitializeComponent();

            edit_fname.Text = Workers.fname;
            edit_lname.Text = Workers.lname;
            edit_mname.Text = Workers.mname;
        }

        private void Click_back(object sender, RoutedEventArgs e)
        {
            Workers window13 = new Workers();
            window13.Show();
            Close();
        }

        private void Click_to_edit(object sender, RoutedEventArgs e)
        {
            if (edit_lname.Text != "" && edit_fname.Text != "" && edit_mname.Text != "" && edit_roleWorker.Text != "" && edit_pincode.Password != string.Empty)
            {
                DataTable dt_worker = Update("update [dbo].[Workers] set LName = '" + edit_lname.Text
                                                               + "', FName = '" + edit_fname.Text
                                                               + "', MName = '" + edit_mname.Text
                                                               + "', RoleWorker = '" + edit_roleWorker.Text
                                                               + "', PinCode = '" + edit_pincode.Password
                                                               + "' WHERE WorkersId = " + Workers.id);
                MessageBox.Show("Данные успешно изменены");
                After_logging_in window2 = new After_logging_in();
                window2.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Все строки должны быть заполнены");
            }
        }

        public DataTable Update(string selectSQL) // функция подключения к базе данных и обработка запросов
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

        private void Click_to_drop(object sender, RoutedEventArgs e)
        {
            DataTable dt_worker = Update("delete from [dbo].[Workers] WHERE WorkersId = " + Workers.id);
            MessageBox.Show("Данные успешно удалены");
            After_logging_in window2 = new After_logging_in();
            window2.Show();
            Close();
        }
    }
}
