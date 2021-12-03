using System;
using static Cash_register.SQLRequest;
using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Close_shift.xaml
    /// </summary>
    public partial class Close_shift : Window
    {
        public Close_shift()
        {
            InitializeComponent();
        }

        private void Click_back(object sender, RoutedEventArgs e)
        {
            Statements statements = new Statements();
            statements.Show();
            Close();
        }

        private void Click_close(object sender, RoutedEventArgs e)
        {
            SQLrequest("Update Statements set FK_Worker = " + Authorization.id + "where StatementsId = (select count(*) from Statements)");
            DateTime date1 = DateTime.Now;
            string month, day;
            if (Convert.ToString(date1.ToShortDateString()).Split('/')[0].Length == 1)
            {
                month = "0" + Convert.ToString(date1.ToShortDateString()).Split('/')[0];
            }
            else
            {
                month = Convert.ToString(date1.ToShortDateString()).Split('/')[0];
            }
            if (Convert.ToString(date1.ToShortDateString()).Split('/')[1].Length == 1)
            {
                day = "0" + Convert.ToString(date1.ToShortDateString()).Split('/')[1];
            }
            else
            {
                day = Convert.ToString(date1.ToShortDateString()).Split('/')[1];
            }
            SQLrequest("Update Statements set WorkingDate = '" + Convert.ToString(date1.ToShortDateString()).Split('/')[2] + month + day + "' where StatementsId = (select count(*) from Statements)");            
            SQLrequest("Insert into Statements values ((select count(ShiftNumber) from Statements) + 1, " + MainWindow.moneyInTheCashRegister + ", " + MainWindow.moneyInTheCashRegister + ", 0, 0, 0, 0, 1, '" + Convert.ToString(date1.ToShortDateString()).Split('/')[2] + month + day + "')");
            Open_shift openShift = new Open_shift();
            openShift.Show();
            Close();
        }
    }
}
