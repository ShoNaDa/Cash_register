using static Cash_register.SQLRequest;
using System.Windows;
using static Cash_register.ChekingValidityProduct;
using System.Collections.Generic;
using System;
using System.Windows.Input;
using System.Windows.Controls.Primitives;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Add_worker.xaml
    /// </summary>
    public partial class Add_worker : Window
    {
        //List
        readonly List<string> Alphabet = new List<string> {
            "А", "а", "Б", "б", "В", "в", "Г", "г", "Д", "д", "Е", "е", "Ё", "ё", "Ж", "ж", "З", "з", "И", "и", "Й", "й",
            "К", "к", "Л", "л", "М", "м", "Н", "н", "О", "о", "П", "п", "Р", "р", "С", "с", "Т", "т", "У", "у", "Ф", "ф",
            "Х", "х", "Ц", "ц", "Ч", "ч", "Ш", "ш", "Щ", "щ", "Ъ", "ъ", "Ы", "ы", "Ь", "ь", "Э", "э", "Ю", "ю", "Я", "я" };

        public Add_worker()
        {
            InitializeComponent();

            add_lname.Focus();
        }

        private void Click_back(object sender, RoutedEventArgs e)
        {
            Workers window13 = new Workers();
            window13.Show();
            Close();
        }

        private void Click_to_add(object sender, RoutedEventArgs e)
        {
            //проверяем нет ли пустых полей
            if (add_lname.Text != "" && add_fname.Text != "" && add_mname.Text != "" && add_roleWorker.Text != "" && add_pincode.Password != string.Empty)
            {
                bool fNameIsOk = false, lNameIsOk = false, mNameIsOk = false;

                //проверяем валидность введенных данных
                if (ValidIsOk(add_fname.Text, fNameIsOk, Alphabet) &&
                    ValidIsOk(add_lname.Text, lNameIsOk, Alphabet) &&
                    ValidIsOk(add_mname.Text, mNameIsOk, Alphabet))
                {
                    SQLrequest("Insert into [dbo].[Workers] values " + "('" + add_lname.Text +
                                                                       "', '" + add_fname.Text +
                                                                       "', '" + add_mname.Text +
                                                                       "', '" + add_roleWorker.Text +
                                                                       "', '" + Authorization.Hash(add_pincode.Password) + "')");
                    MessageBox.Show("Данные успешно добавлены");

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

        private void Window15_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Button_back.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            if (e.Key == Key.Enter && add_fname.Focusable != true && add_lname.Focusable != true && add_mname.Focusable != true
                && add_roleWorker.Focusable != true && add_pincode.Focusable)
            {
                Button_add.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            else if (e.Key == Key.Enter && add_lname.Focusable)
            {
                add_lname.Focusable = false;
                add_fname.Focus();
            }
            else if (e.Key == Key.Enter && add_fname.Focusable)
            {
                add_fname.Focusable = false;
                add_mname.Focus();
            }
            else if (e.Key == Key.Enter && add_mname.Focusable)
            {
                add_mname.Focusable = false;
                add_roleWorker.Focus();
            }
            else if (e.Key == Key.Enter && add_roleWorker.Focusable)
            {
                add_roleWorker.Focusable = false;
                add_pincode.Focus();
            }
        }
    }
}