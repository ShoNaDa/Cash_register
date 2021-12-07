using System;
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
        List<string> Alphabet = new List<string> {
            "А", "а", "Б", "б", "В", "в", "Г", "г", "Д", "д", "Е", "е", "Ё", "ё", "Ж", "ж", "З", "з", "И", "и", "Й", "й",
            "К", "к", "Л", "л", "М", "м", "Н", "н", "О", "о", "П", "п", "Р", "р", "С", "с", "Т", "т", "У", "у", "Ф", "ф",
            "Х", "х", "Ц", "ц", "Ч", "ч", "Ш", "ш", "Щ", "щ", "Ъ", "ъ", "Ы", "ы", "Ь", "ь", "Э", "э", "Ю", "ю", "Я", "я" };

        public Edit_worker()
        {
            InitializeComponent();

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
                bool FNameIsOk = false, LNameIsOk = false, MNameIsOk = false;
                //проверяем правильно ли написано имя
                for (int i = 0; i < edit_fname.Text.Length; i++)
                {
                    if (FNameIsOk)
                    {
                        FNameIsOk = false;
                    }
                    for (int j = 0; j < Alphabet.Count; j++)
                    {
                        if (Convert.ToString(edit_fname.Text[i]).Contains(Alphabet[j]))
                        {
                            FNameIsOk = true;
                            break;
                        }
                    }
                    if (FNameIsOk == false)
                    {
                        break;
                    }
                }
                //проверяем правильно ли написана фамилия
                for (int i = 0; i < edit_lname.Text.Length; i++)
                {
                    if (LNameIsOk)
                    {
                        LNameIsOk = false;
                    }
                    for (int j = 0; j < Alphabet.Count; j++)
                    {
                        if (Convert.ToString(edit_lname.Text[i]).Contains(Alphabet[j]))
                        {
                            LNameIsOk = true;
                            break;
                        }
                    }
                    if (LNameIsOk == false)
                    {
                        break;
                    }
                }
                //проверяем правильно ли написано отчество
                for (int i = 0; i < edit_mname.Text.Length; i++)
                {
                    if (MNameIsOk)
                    {
                        MNameIsOk = false;
                    }
                    for (int j = 0; j < Alphabet.Count; j++)
                    {
                        if (Convert.ToString(edit_mname.Text[i]).Contains(Alphabet[j]))
                        {
                            MNameIsOk = true;
                            break;
                        }
                    }
                    if (MNameIsOk == false)
                    {
                        break;
                    }
                }
                //если все ок...
                if (FNameIsOk && LNameIsOk && MNameIsOk)
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