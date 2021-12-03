using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

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
        }

        private void Click_open(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void openShift_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_open.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
    }
}
