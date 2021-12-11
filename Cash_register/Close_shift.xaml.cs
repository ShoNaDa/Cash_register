using static Cash_register.SQLRequest;
using static Cash_register.DateFunc;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls.Primitives;

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
            //изменяем ID сотрудника на всякий случай в таблице смены
            SQLrequest("Update [Shift] set FK_workerId = " + Authorization.id + " where ShiftId = (select max(ShiftId) from [Shift])");

            //изменяем дату в таблице баланса
            SQLrequest("Update BalanceAfterCloseCashRegister set [Date] = '" + DateFunction()[2] + DateFunction()[0] + DateFunction()[1] + "' where BalanceId = (select max(BalanceId) from BalanceAfterCloseCashRegister)");
            
            //изменяем дату в таблице смены
            SQLrequest("Update [Shift] set CloseShiftDate = '" + DateFunction()[2] + DateFunction()[0] + DateFunction()[1] + "' where ShiftId = (select max(ShiftId) from [Shift])");

            Refund_of_products.refundOfShift = 0;

            Open_shift openShift = new Open_shift();
            openShift.Show();
            Close();
        }

        private void CloseShift_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Button_back.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            if (e.Key == Key.Enter)
            {
                Button_close.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
    }
}
