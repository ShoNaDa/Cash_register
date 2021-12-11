using static Cash_register.SQLRequest;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Data;
using System.Collections.Generic;

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

            //Создаю строку приветствия
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
            if (MainWindow.isAdmin)
            {
                RoleVerification(MainWindow.Workers_ID_admins, List_of_admin.index, MainWindow.isAdmin);
            }
            else
            {
                RoleVerification(MainWindow.Workers_ID_cashiers, List_of_cashiers.index, MainWindow.isAdmin);
            }
        }

        //функция, которая проверяет правильность пароля и говорит какое окно следующим открыть
        public void RoleVerification(List<int> RoleEmployee, int index, bool isAdmin)
        {
            //узнаем информацию о пользователе
            DataTable dt_role = SQLrequest("SELECT * FROM [dbo].[Workers] where WorkerId = " + GiveIdEmployee(RoleEmployee, index));

            //проверяем, что введенный пароль, если его захешировать будет соответствовать с строкой в бд
            //далее просто открываю окно
            if (Hash(password.Password) == (string)dt_role.Rows[0][5])
            {
                if (isAdmin)
                {
                    After_logging_in admin = new After_logging_in();
                    admin.Show();
                }
                else
                {
                    After_login_in_cashier cashier = new After_login_in_cashier();
                    cashier.Show();
                }
                Close();
            }
            else
            {
                MessageBox.Show("Неправильный пароль");
            }
        }

        //функция получения ID сотрудника, который входит (типо авторизация)
        public static int GiveIdEmployee(List<int> RoleEmployee, int index)
        {
            //волшебным образом узнаю id пользователя
            int counter = 0;
            foreach (int i in RoleEmployee)
            {
                if (counter == index)
                {
                    id = i;
                }
                counter++; ;
            }

            return id;
        }

        //функция хэширования
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

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_enter.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
    }
}