using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using static Cash_register.SQLRequest;
using static Cash_register.DateFunc;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Open_shift.xaml
    /// </summary>
    public partial class Open_shift : Window
    {
        public Open_shift()
        {
            InitializeComponent();

            moneyInTheCashRegister.Text = MainWindow.moneyInTheCashRegister + "₽ в кассе";

            //добавляем записаь в таблицу баланса
            SQLrequest("Insert into BalanceAfterCloseCashRegister values (" + MainWindow.moneyInTheCashRegister + ", " 
                + MainWindow.moneyInTheCashRegister + ", 0, 0, '" + DateFunction()[2] + DateFunction()[0] + DateFunction()[1] + "')");

            //добавляет запись вв таблицу смены
            SQLrequest("Insert into [Shift] values ((select max(ShiftNumber) from [Shift]) + 1, " + Authorization.id +
                ", (select max(ShiftNumber) from [Shift]) + 1, '" + DateFunction()[2] + DateFunction()[0] + DateFunction()[1] + "')");
        }

        private void Click_open(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void OpenShift_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_open.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
    }
}
