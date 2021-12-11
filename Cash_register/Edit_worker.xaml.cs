using static Cash_register.ChekingValidityProduct;
using System.Collections.Generic;
using static Cash_register.SQLRequest;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls.Primitives;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Edit_worker.xaml
    /// </summary>
    public partial class Edit_worker : Window
    {
        private readonly List<string> Alphabet = new List<string> {
            "А", "а", "Б", "б", "В", "в", "Г", "г", "Д", "д", "Е", "е", "Ё", "ё", "Ж", "ж", "З", "з", "И", "и", "Й", "й",
            "К", "к", "Л", "л", "М", "м", "Н", "н", "О", "о", "П", "п", "Р", "р", "С", "с", "Т", "т", "У", "у", "Ф", "ф",
            "Х", "х", "Ц", "ц", "Ч", "ч", "Ш", "ш", "Щ", "щ", "Ъ", "ъ", "Ы", "ы", "Ь", "ь", "Э", "э", "Ю", "ю", "Я", "я" };

        public Edit_worker()
        {
            InitializeComponent();

            //выводим инфу о сотруднике
            edit_fname.Text = Workers.fname;
            edit_lname.Text = Workers.lname;
            edit_mname.Text = Workers.mname;

            edit_lname.Focus();
        }

        private void Click_back(object sender, RoutedEventArgs e)
        {
            Workers window13 = new Workers();
            window13.Show();
            Close();
        }

        private void Click_to_edit(object sender, RoutedEventArgs e)
        {
            //проверяем нет ли пустых полей
            if (edit_lname.Text != "" && edit_fname.Text != "" && edit_mname.Text != "" && edit_roleWorker.Text != "" && edit_pincode.Password != string.Empty)
            {
                bool fNameIsOk = false, lNameIsOk = false, mNameIsOk = false;
                
                //если все ок...
                if (ValidIsOk(edit_fname.Text, fNameIsOk, Alphabet) &&
                    ValidIsOk(edit_lname.Text, lNameIsOk, Alphabet) &&
                    ValidIsOk(edit_mname.Text, mNameIsOk, Alphabet))
                {
                    SQLrequest("Update [dbo].[Workers] set LName = '" + edit_lname.Text
                                                                   + "', FName = '" + edit_fname.Text
                                                                   + "', MName = '" + edit_mname.Text
                                                                   + "', RoleWorker = '" + edit_roleWorker.Text
                                                                   + "', PinCode = '" + Authorization.Hash(edit_pincode.Password)
                                                                   + "' WHERE WorkerId = " + Workers.id);
                    MessageBox.Show("Данные успешно изменены");

                    After_logging_in window2 = new After_logging_in();
                    window2.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Неправильный формат");
                }
            }
            else
            {
                MessageBox.Show("Все строки должны быть заполнены");
            }
        }
        private void Click_to_drop(object sender, RoutedEventArgs e)
        {
            //чтоб админ сам себя удалить не мог
            if (Workers.id != Authorization.id)
            {
                //удаляем данные из таблицу сотрудники
                SQLrequest("Delete from [dbo].[Workers] WHERE WorkerId = " + Workers.id);

                MessageBox.Show("Данные успешно удалены");

                After_logging_in window2 = new After_logging_in();
                window2.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void Window14_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Button_back.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            if (e.Key == Key.Enter && edit_fname.Focusable != true && edit_fname.Focusable != true && edit_mname.Focusable != true
                && edit_roleWorker.Focusable != true && edit_pincode.Focusable)
            {
                Button_edit.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            else if (e.Key == Key.Enter && edit_lname.Focusable)
            {
                edit_lname.Focusable = false;
                edit_fname.Focus();
            }
            else if (e.Key == Key.Enter && edit_fname.Focusable)
            {
                edit_fname.Focusable = false;
                edit_mname.Focus();
            }
            else if (e.Key == Key.Enter && edit_mname.Focusable)
            {
                edit_mname.Focusable = false;
                edit_roleWorker.Focus();
            }
            else if (e.Key == Key.Enter && edit_roleWorker.Focusable)
            {
                edit_roleWorker.Focusable = false;
                edit_pincode.Focus();
            }
        }
    }
}