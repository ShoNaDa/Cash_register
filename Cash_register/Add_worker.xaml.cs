using static Cash_register.SQLRequest;
using System.Windows;
using System.Security.Cryptography;
using System.Text;
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
        List<string> Alphabet = new List<string> {
            "А", "а", "Б", "б", "В", "в", "Г", "г", "Д", "д", "Е", "е", "Ё", "ё", "Ж", "ж", "З", "з", "И", "и", "Й", "й",
            "К", "к", "Л", "л", "М", "м", "Н", "н", "О", "о", "П", "п", "Р", "р", "С", "с", "Т", "т", "У", "у", "Ф", "ф",
            "Х", "х", "Ц", "ц", "Ч", "ч", "Ш", "ш", "Щ", "щ", "Ъ", "ъ", "Ы", "ы", "Ь", "ь", "Э", "э", "Ю", "ю", "Я", "я" };

        public Add_worker()
        {
            InitializeComponent();
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
                bool FNameIsOk = false, LNameIsOk = false, MNameIsOk = false;
                //проверяем правильно ли написано имя
                for (int i = 0; i < add_fname.Text.Length; i++)
                {
                    if (FNameIsOk)
                    {
                        FNameIsOk = false;
                    }
                    for (int j = 0; j < Alphabet.Count; j++)
                    {
                        if (Convert.ToString(add_fname.Text[i]).Contains(Alphabet[j]))
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
                for (int i = 0; i < add_lname.Text.Length; i++)
                {
                    if (LNameIsOk)
                    {
                        LNameIsOk = false;
                    }
                    for (int j = 0; j < Alphabet.Count; j++)
                    {
                        if (Convert.ToString(add_lname.Text[i]).Contains(Alphabet[j]))
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
                for (int i = 0; i < add_mname.Text.Length; i++)
                {
                    if (MNameIsOk)
                    {
                        MNameIsOk = false;
                    }
                    for (int j = 0; j < Alphabet.Count; j++)
                    {
                        if (Convert.ToString(add_mname.Text[i]).Contains(Alphabet[j]))
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
                    //переводим строку в байт-массим  
                    byte[] bytes = Encoding.Unicode.GetBytes(add_pincode.Password);

                    //создаем объект для получения средст шифрования  
                    MD5CryptoServiceProvider CSP =
                        new MD5CryptoServiceProvider();

                    //вычисляем хеш-представление в байтах  
                    byte[] byteHash = CSP.ComputeHash(bytes);

                    //создаем пустую строку
                    string hash = string.Empty;

                    //формируем одну цельную строку из массива  
                    foreach (byte b in byteHash)
                        hash += string.Format("{0:x2}", b);
                    SQLrequest("Insert into [dbo].[Workers] values " + "('" + add_lname.Text +
                                                                       "', '" + add_fname.Text +
                                                                       "', '" + add_mname.Text +
                                                                       "', '" + add_roleWorker.Text +
                                                                       "', '" + hash + "')");
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

        private void window15_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Button_back.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            if (e.Key == Key.Enter)
            {
                Button_add.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
    }
}