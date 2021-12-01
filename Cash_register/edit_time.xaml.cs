using System;
using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для edit_time.xaml
    /// </summary>
    public partial class edit_time : Window
    {
        public static int dateAndTime = 0;
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
            if (List_of_time.SelectedIndex == 0)
            {
                dateAndTime = 1;
            }
            else if (List_of_time.SelectedIndex == 1)
            {
                dateAndTime = 2;
            }
            else if (List_of_time.SelectedIndex == 2)
            {
                dateAndTime = 3;
            }
            else if (List_of_time.SelectedIndex == 3)
            {
                dateAndTime = 4;
            }
            else if (List_of_time.SelectedIndex == 4)
            {
                dateAndTime = 5;
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
