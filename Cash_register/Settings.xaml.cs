using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }

        public void Click_back(object sender, RoutedEventArgs e)
        {
            After_logging_in window2 = new After_logging_in();
            window2.Show();
            Close();
        }

        public void Click_to_time(object sender, RoutedEventArgs e)
        {

        }

        public void Click_to_employee(object sender, RoutedEventArgs e)
        {
            Workers window13 = new Workers();
            window13.Show();
            Close();
        }
    }
}
