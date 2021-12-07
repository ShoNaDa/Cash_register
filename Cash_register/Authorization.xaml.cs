using static Cash_register.SQLRequest;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Data;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public static int id = 0;
        public Authorization()
        {
            InitializeComponent();

            string fName = MainWindow.FIO_worker.Split(' ')[1];
            string lName = MainWindow.FIO_worker.Split(' ')[0];
            string mName = MainWindow.FIO_worker.Split(' ')[2];
            Worker.Text = string.Concat(lName, " ", fName.Substring(0, 1), ". ", mName.Substring(0, 1), ". ");

            password.Focus();
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
                
                foreach (int i in MainWindow.Workers_ID_admins)
                {
                    if (counter == List_of_admin.index)
                    {
                        id = i;
                    }
                    counter++; ;
                }

                DataTable dt_admins = SQLrequest("SELECT * FROM [dbo].[Workers] where roleWorker = 'Администратор' and WorkerId = " + id);
                for (int i = 0; i < dt_admins.Rows.Count; i++)
                {
                    if (Hash(password.Password) == (string)dt_admins.Rows[i][5])
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
                foreach (int i in MainWindow.Workers_ID_cashiers)
                {
                    if (counter == List_of_cashiers.index)
                    {
                        id = i;
                    }
                    counter++; ;
                }

                DataTable dt_cashiers = SQLrequest("SELECT * FROM [dbo].[Workers] where WorkerId = " + id);
                for (int i = 0; i < dt_cashiers.Rows.Count; i++)
                {
                    if (Hash(password.Password) == (string)dt_cashiers.Rows[i][5])
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
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_enter.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
        public static string Hash(string password)
        {
            //переводим строку в байт-массим  
            byte[] bytes = Encoding.Unicode.GetBytes(password);

            //создаем объект для получения средст шифрования  
            MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);

            //создаем пустую строку
            string hash = string.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
            {
                hash += string.Format("{0:x2}", b);
            }
            return hash;
        }
    }
}
