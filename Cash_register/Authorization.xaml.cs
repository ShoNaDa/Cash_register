using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
            string fName = MainWindow.FIO_cashier.Split(' ')[1];
            string lName = MainWindow.FIO_cashier.Split(' ')[0];
            string mName = MainWindow.FIO_cashier.Split(' ')[2];
            Worker.Text = string.Concat(lName, " ", fName.Substring(0, 1), ". ", mName.Substring(0, 1), ". ");
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
                After_logging_in window2 = new After_logging_in();
                window2.Show();
                Close();
            }
            else if (MainWindow.IsAdmin == false)
            {
                After_login_in_cashier window9 = new After_login_in_cashier();
                window9.Show();
                Close();
            }
            
        }
    }
}
