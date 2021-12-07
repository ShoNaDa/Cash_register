using System.Windows;
using static Cash_register.SQLRequest;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для TipOfPament.xaml
    /// </summary>
    public partial class TipOfPament : Window
    {
        public TipOfPament()
        {
            InitializeComponent();
        }

        private void Click_byCash(object sender, RoutedEventArgs e)
        {
            SQLrequest("Insert into Pament values ('Наличные')");
            Close();
        }

        private void Click_byCard(object sender, RoutedEventArgs e)
        {
            SQLrequest("Insert into Pament values ('Банковская карта')");
            Close();
        }

        private void Click_back(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
