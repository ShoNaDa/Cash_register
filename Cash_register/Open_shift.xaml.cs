using System.Windows;

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
    }
}
