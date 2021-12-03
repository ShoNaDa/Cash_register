using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для List_of_admin.xaml
    /// </summary>
    public partial class List_of_admin : Window
    {
        public static int index = 0;
        public List_of_admin()
        {
            InitializeComponent();

            foreach (string i in MainWindow.Admins)
            {
                List_of_admins.Items.Add(i);
            }
        }

        private void Click_back(object sender, RoutedEventArgs e)
        {
            MainWindow.Admins.Clear();
            MainWindow.Cashiers.Clear();
            MainWindow Cash_register = new MainWindow();
            Cash_register.Show();
            Close();
        }

        private void Click_to_next(object sender, RoutedEventArgs e)
        {
            index = List_of_admins.SelectedIndex;
            MainWindow.Admins.Clear();
            MainWindow.Cashiers.Clear();
            if (List_of_admins.SelectedIndex != -1)
            {
                MainWindow.FIO_worker = (string)List_of_admins.SelectedValue;
                Authorization Autorization = new Authorization();
                Autorization.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Выберите сотрудника");
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_next.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            if (e.Key == Key.Escape)
            {
                Button_back.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
    }
}
