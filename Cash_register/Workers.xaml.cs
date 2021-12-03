using System.Data;
using static Cash_register.SQLRequest;
using System.Windows;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Workers.xaml
    /// </summary>
    public partial class Workers : Window
    {
        public static string lname, fname, mname;
        public static int id = 0;
        public Workers()
        {
            InitializeComponent();

            DataTable dt_workers = SQLrequest("SELECT * FROM [dbo].[Workers]");
            for (int i = 0; i < dt_workers.Rows.Count; i++)
            {
                MainWindow.Workers.Add((string)dt_workers.Rows[i][1] + " " + ((string)dt_workers.Rows[i][2]).Substring(0, 1) + ". " + ((string)dt_workers.Rows[i][3]).Substring(0, 1) + ". (" + (string)dt_workers.Rows[i][4] + ")");
                MainWindow.Workers_ID.Add((int)dt_workers.Rows[i][0]);
            }

            foreach (string i in MainWindow.Workers)
            {
                List_of_workers.Items.Add(i);
            }
        }

        private void Click_back(object sender, RoutedEventArgs e)
        {
            MainWindow.Workers.Clear();
            Settings window7 = new Settings();
            window7.Show();
            Close();
        }

        private void Click_to_add_worker(object sender, RoutedEventArgs e)
        {
            MainWindow.Workers.Clear();
            Add_worker window15 = new Add_worker();
            window15.Show();
            Close();
        }
        private void Click_to_edit_worker(object sender, RoutedEventArgs e)
        {
            MainWindow.Workers.Clear();
            if (List_of_workers.SelectedIndex != -1)
            {
                int index = List_of_workers.SelectedIndex;
                int counter = 0;
                foreach (int i in MainWindow.Workers_ID)
                {
                    if (counter == index)
                    {
                        id = i;
                    }
                    counter++; ;
                }
                DataTable dt_worker = SQLrequest("SELECT * FROM [dbo].[Workers] where WorkersId = " + id);
                fname = (string)dt_worker.Rows[0][2];
                lname = (string)dt_worker.Rows[0][1];
                mname = (string)dt_worker.Rows[0][3];
                MainWindow.Workers_ID.Clear();
                Edit_worker window14 = new Edit_worker();
                window14.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Выберите сотрудника");
            }
        }
    }
}
