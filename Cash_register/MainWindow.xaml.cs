using System;
using System.Collections.Generic;
using System.Data;
using static Cash_register.SQLRequest;
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
        public static List<int> Workers_ID = new List<int>(1);
        public static List<int> Workers_ID_cashiers = new List<int>(1);
        public static List<int> Workers_ID_admins = new List<int>(1);
        public static List<string> Cashiers = new List<string>(1);
        public static List<string> Admins = new List<string>(1);
        public static List<string> Products = new List<string>(1);
        //bool
        public static bool isCashier;
        public static bool isAdmin;
        public static bool theShiftIsOpen;
        //string
        public static string FIO_worker;
        //int
        public static int shiftNumber;
        public static int productId;
        //double
        public static double moneyInTheCashRegister;
        public static double moneyAtTheBeginningOfTheShift;
        public static double sales;
        public static double refund;
        public static double withdrawals;
        public static double deposits;

        public MainWindow()
        {
            InitializeComponent();

            //если это первая смена, то добавляем начальные значения в таблицы Смена и Баланс (они изменятся) и создаем тестового админа
            DataTable dt_shiftNumber = SQLrequest("SELECT count(ShiftNumber) FROM [Shift]");
            if (Convert.ToInt32(dt_shiftNumber.Rows[0][0]) == 0)
            {
                SQLrequest("Insert into BalanceAfterCloseCashRegister values (0, 0, 0, 0, getdate())");

                SQLrequest("Insert into Workers values ('Тест', 'Тест', 'Тест', 'Администратор', '95751a2e765809e6221e3249319cee73')");
                //пароль 12314

                SQLrequest("Insert into [Shift] values (1, 1, 1, getdate())");
            }

            //Составляем лист существующих админов
            DataTable dt_admins = SQLrequest("SELECT * FROM [dbo].[Workers] WHERE roleWorker = 'Администратор'");
            CreatListEmployee(Admins, dt_admins, Workers_ID_admins);

            //Составляем лист существующих кассиров
            DataTable dt_cashiers = SQLrequest("SELECT * FROM [dbo].[Workers] where roleWorker = 'Кассир'");
            CreatListEmployee(Cashiers, dt_cashiers, Workers_ID_cashiers);
        }

        //функция для составления листа со строками типа: Фамилия Имя Отчество, а также добавления ID сотрудника в лист
        public static void CreatListEmployee(List<string> InfoEmployee, DataTable employee, List<int> IdEmployee)
        {
            for (int i = 0; i < employee.Rows.Count; i++)
            {
                InfoEmployee.Add((string)employee.Rows[i][1] + " " + (string)employee.Rows[i][2] + " " + (string)employee.Rows[i][3]);
                IdEmployee.Add((int)employee.Rows[i][0]);
            }
        }

        public void Click_to_cashier(object sender, RoutedEventArgs e)
        {
            isAdmin = false;

            List_of_cashiers window11 = new List_of_cashiers();
            window11.Show();
            Close();
        }

        public void Click_to_admin(object sender, RoutedEventArgs e)
        {
            isAdmin = true;

            List_of_admin window12 = new List_of_admin();
            window12.Show();
            Close();
        }
    }
}
