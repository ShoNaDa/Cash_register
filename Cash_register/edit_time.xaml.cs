using System;
using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для edit_time.xaml
    /// </summary>
    public partial class edit_time : Window
    {
        public static string dateAndTime;
        public edit_time()
        {
            InitializeComponent();
        }

        private void Click_back(object sender, RoutedEventArgs e)
        {
            Settings window7 = new Settings();
            window7.Show();
            Close();
        }

        private void Click_to_save(object sender, RoutedEventArgs e)
        {
            DateTime date1 = DateTime.Now;
            if (List_of_time.SelectedIndex == 0)
            {
                dateAndTime = date1.ToLongDateString();
            }
            else if (List_of_time.SelectedIndex == 1)
            {
                dateAndTime = date1.ToShortDateString();
            }
            else if (List_of_time.SelectedIndex == 2)
            {
                dateAndTime = date1.ToLongTimeString();
            }
            else if (List_of_time.SelectedIndex == 3)
            {
                dateAndTime = date1.ToShortTimeString();
            }
            else if (List_of_time.SelectedIndex == 4)
            {
                dateAndTime = Convert.ToString(date1.ToLocalTime());
            }
            if (List_of_time.SelectedIndex != -1)
            {
                MessageBox.Show("Изменения сохранены");
                Settings window7 = new Settings();
                window7.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Выберите форматa");
            }
        }
    }
}
