using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Security.Cryptography;
using System.Text;

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
            if (add_lname.Text != "" && add_fname.Text != "" && add_mname.Text != "" && add_roleWorker.Text != "" && add_pincode.Password != string.Empty)
            {
                //переводим строку в байт-массим  
                byte[] bytes = Encoding.Unicode.GetBytes(add_pincode.Password);

                //создаем объект для получения средст шифрования  
                MD5CryptoServiceProvider CSP =
                    new MD5CryptoServiceProvider();

                //вычисляем хеш-представление в байтах  
                byte[] byteHash = CSP.ComputeHash(bytes);

                //создаем пустую строку
                string hash = string.Empty;

                //формируем одну цельную строку из массива  
                foreach (byte b in byteHash)
                    hash += string.Format("{0:x2}", b);
                DataTable dt_worker = Insert("Insert into [dbo].[Workers] values " +
                                                                          "('" + add_lname.Text +
                                                                        "', '" + add_fname.Text +
                                                                        "', '" + add_mname.Text +
                                                                        "', '" + add_roleWorker.Text +
                                                                        "', '" + hash + "')");
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
