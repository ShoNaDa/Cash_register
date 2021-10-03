using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Amount_of_money.xaml
    /// </summary>
    public partial class Amount_of_money : Window
    {
        public Amount_of_money()
        {
            InitializeComponent();
        }

        public void Click_back(object sender, RoutedEventArgs e)
        {
            Statements window6 = new Statements();
            window6.Show();
            Close();
        }
    }
}
