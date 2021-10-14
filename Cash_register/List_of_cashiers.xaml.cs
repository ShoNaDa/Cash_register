using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для List_of_cashiers.xaml
    /// </summary>
    public partial class List_of_cashiers : Window
    {
        public static int index = 0;
        public List_of_cashiers()
        {
            InitializeComponent();

            foreach (string i in MainWindow.Cashiers)
            {
                List_of_cashier.Items.Add(i);
            }
        }

        private void Click_to_next(object sender, RoutedEventArgs e)
        {
            index = List_of_cashier.SelectedIndex;
            MainWindow.Cashiers.Clear();
            MainWindow.Admins.Clear();
            if (List_of_cashier.SelectedIndex != -1)
            {
                MainWindow.FIO_worker = (string)List_of_cashier.SelectedValue;
                Authorization Autorization = new Authorization();
                Autorization.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Выберите сотрудника");
            }
        }

        private void Click_back(object sender, RoutedEventArgs e)
        {
            MainWindow.Cashiers.Clear();
            MainWindow.Admins.Clear();
            MainWindow Cash_register = new MainWindow();
            Cash_register.Show();
            Close();
        }
    }
}
