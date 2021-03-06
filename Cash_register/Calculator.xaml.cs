using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Calculator.xaml
    /// </summary>
    public partial class Calculator : Window
    {
        //переменная, в которую заполняется выражение
        private static string equation = "";
        private static bool pointIsOk = true;

        public Calculator()
        {
            InitializeComponent();
        }

        //Метод нажатия на выбранный знак
        public void Buttons_of_sings(object sender, RoutedEventArgs e)
        {
            Signs(Convert.ToString(((Button)sender).Content));

            //ставим фокус на равно
            Equal.Focus();
        }

        //Метод нажатия на выбранную цифру
        public void Buttons_of_numbers(object sender, RoutedEventArgs e)
        {
            Numbers(Convert.ToString(((Button)sender).Content));

            //ставим фокус на равно
            Equal.Focus();
        }

        //функция для предотвращения багов с цифрами
        public static string NumberIsOk(string number, string equation)
        {
            if (equation == "")
            {
                equation = number;
            }
            else if (equation[equation.Length - 1] == '0' && equation.Length == 1)
            {
                equation = equation.Substring(0, equation.Length - 1);
                equation += number;
            }
            else if (equation[equation.Length - 1] == '0' && equation[equation.Length - 2] != '1' &&
                equation[equation.Length - 2] != '2' && equation[equation.Length - 2] != '3' && 
                equation[equation.Length - 2] != '4' && equation[equation.Length - 2] != '5' && 
                equation[equation.Length - 2] != '6' && equation[equation.Length - 2] != '7' && 
                equation[equation.Length - 2] != '8' && equation[equation.Length - 2] != '9' && equation[equation.Length - 2] != '0')
            {
                equation = equation.Substring(0, equation.Length - 1);
                equation += number;
            }
            else
            {
                equation += number;
            }

            return equation;
        }

        //функция для промежуточной записи выражения и проверки на '.' и ','
        public void Numbers(string number)
        {
            equation = NumberIsOk(number, equation);

            Reply_line.Text = equation;

            //чтобы всегда стояла точка вместо запятой
            Reply_line.Text = Reply_line.Text.Replace(',', '.');
        }

        //функция для предотвращения багов с точкой
        public static bool SignPointIsOk(string sign, string equation)
        {
            pointIsOk = true;

            //если пытаемся точку после другого знака поставить
            if (sign == "." && (equation[equation.Length - 1] == '/' || equation[equation.Length - 1] == '*'
                || equation[equation.Length - 1] == '-' || equation[equation.Length - 1] == '+'))
            {
                pointIsOk = false;
            }
            else if (sign == "." && pointIsOk)
            {
                //проверяем нет ли двух точек в одном числе
                for (int i = equation.Length - 1; i >= 0; i--)
                {
                    if (equation[i] == '/' || equation[i] == '*' || equation[i] == '-' || equation[i] == '+')
                    {
                        break;
                    }
                    else if (equation[i] == '.')
                    {
                        pointIsOk = false;
                    }
                }
            }

            return pointIsOk;
        }

        //функция для проверки правильного написания знаков
        public static string SignsIsOk(string sign, string equation)
        {
            //если мы пытаемся поставить знак в самом начале
            if (equation == "" || equation == "-")
            {
                //минус можно поставить
                if (sign == "-")
                {
                    equation = sign;
                }
                else
                {
                    MessageBox.Show(Convert.ToString("Ошибка!"));
                }
            }
            //если пытаемся поставить знак после знака, то он заменяется
            else if (equation[equation.Length - 1] == '.' || equation[equation.Length - 1] == '/' || 
                equation[equation.Length - 1] == '*' || equation[equation.Length - 1] == '+' || equation[equation.Length - 1] == '-')
            {
                equation = equation.Substring(0, equation.Length - 1);
                equation += sign;
            }
            else
            {
                equation += sign;
            }

            return equation;
        }

        //фнкция для промежуточной записи выражения
        public void Signs(string sign)
        {
            //если точку поставили сразу
            if (sign == "." && equation == "")
            {
                equation = "0.";
            }

            Reply_line.Text = equation;

            //проверка, что точка введена правильно
            if (!SignPointIsOk(sign, equation))
            {
                MessageBox.Show(Convert.ToString("Ошибка!"));
            }

            equation = SignsIsOk(sign, equation);

            Reply_line.Text = equation;

            //чтобы всегда стояла точка вместо запятой
            Reply_line.Text = Reply_line.Text.Replace(',', '.');
        }

        //функция для проверки правильного написания нуля
        public static string ZiroIsOk(string equation)
        {
            //если ноль вначале
            if (equation == "" || equation == "0")
            {
                equation = "0";
            }
            else if (equation[equation.Length - 1] == '0' && (equation[equation.Length - 2] == '-' || 
                equation[equation.Length - 2] == '*' || equation[equation.Length - 2] == '+'))
            {
                equation = equation.Substring(0, equation.Length - 1);
                equation += "0";
            }
            else if (equation[equation.Length - 1] == '/')
            {
                MessageBox.Show(Convert.ToString("Ошибка!"));
            }
            else
            {
                equation += "0";
            }

            return equation;
        }

        //нажата цифра 0
        private void Click_to_zero(object sender, RoutedEventArgs e)
        {
            equation = ZiroIsOk(equation);

            Reply_line.Text = equation;

            //чтобы всегда стояла точка вместо запятой
            Reply_line.Text = Reply_line.Text.Replace(',', '.');

            //ставим фокус на равно
            Equal.Focus();
        }

        //минифункция для проверки нажатия на равно
        public static bool EqualIsOk(string equation)
        {
            bool equalIsOk = true;

            if (equation == "")
            {
                equalIsOk = false;
            }

            else if (equation[equation.Length - 1] == '.' || equation[equation.Length - 1] == '/' || equation[equation.Length - 1] == '*' || equation[equation.Length - 1] == '+' || equation[equation.Length - 1] == '-')
            {
                equalIsOk = false;
            }

            return equalIsOk;
        }

        //нажата кнопка равно
        private void Click_to_equal(object sender, RoutedEventArgs e)
        {
            if (EqualIsOk(equation))
            {
                try
                {
                    //Ответ             
                    var result = new DataTable().Compute(equation, null);
                    equation = Convert.ToString(result);
                    Reply_line.Text = Convert.ToString(result);
                }
                catch (FormatException message)
                {
                    //сообщение об ошибке если шо
                    MessageBox.Show(Convert.ToString(message));
                }
            }

            //чтобы всегда стояла точка вместо запятой
            Reply_line.Text = Reply_line.Text.Replace(',', '.');

            //ставим фокус на равно
            Equal.Focus();
        }

        //нажата кнопка стереть все
        private void Click_to_CA(object sender, RoutedEventArgs e)
        {
            equation = "";
            Reply_line.Text = "0";
        }

        public void Click_back(object sender, RoutedEventArgs e)
        {
            if (MainWindow.isCashier)
            {
                After_login_in_cashier window9 = new After_login_in_cashier();
                window9.Show();
                Close();
            }
            else
            {
                After_logging_in window2 = new After_logging_in();
                window2.Show();
                Close();
            }
        }

        //функции нажатия на кнопки
        private void Grid_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == "1")
            {
                equation += "1";
                Reply_line.Text = equation;
            }
            else if (e.Text == "2")
            {
                equation += "2";
                Reply_line.Text = equation;
            }
            else if (e.Text == "3")
            {
                equation += "3";
                Reply_line.Text = equation;
            }
            else if (e.Text == "4")
            {
                equation += "4";
                Reply_line.Text = equation;
            }
            else if (e.Text == "5")
            {
                equation += "5";
                Reply_line.Text = equation;
            }
            else if (e.Text == "6")
            {
                equation += "6";
                Reply_line.Text = equation;
            }
            else if (e.Text == "7")
            {
                equation += "7";
                Reply_line.Text = equation;
            }
            else if (e.Text == "8")
            {
                equation += "8";
                Reply_line.Text = equation;
            }
            else if (e.Text == "9")
            {
                equation += "9";
                Reply_line.Text = equation;
            }
            else if (e.Text == "0")
            {
                Click_to_zero(sender, e);
            }
            else if (e.Text == "+")
            {
                Signs("+");
            }
            else if (e.Text == "-")
            {
                Signs("-");
            }
            else if (e.Text == "*")
            {
                Signs("*");
            }
            else if (e.Text == "/")
            {
                Signs("/");
            }
            else if (e.Text == "=")
            {
                Click_to_equal(sender, e);
            }
            else if (e.Text == ".")
            {
                Signs(".");
            }

            //чтобы всегда стояла точка вместо запятой
            Reply_line.Text = Reply_line.Text.Replace(',', '.');

            //ставим фокус на равно
            Equal.Focus();
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (equation.Length != 0)
            {
                if (e.Key == Key.Back)
                {
                    Click_to_clear(sender, e);
                }
            }

            if (e.Key == Key.Enter)
            {
                Equal.Focus();
                Click_to_equal(sender, e);
            }
            else if (e.Key == Key.Delete)
            {
                Click_to_CA(sender, e);
            }

            //чтобы всегда стояла точка вместо запятой
            Reply_line.Text = Reply_line.Text.Replace(',', '.');

            //ставим фокус на равно
            Equal.Focus();

            if (e.Key == Key.Escape)
            {
                Button_back.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }

        //кнопка стереть
        private void Click_to_clear(object sender, RoutedEventArgs e)
        {
            if (equation != "")
            {
                equation = equation.Substring(0, equation.Length - 1);
                Reply_line.Text = equation;

                //чтобы всегда стояла точка вместо запятой
                Reply_line.Text = Reply_line.Text.Replace(',', '.');
            }

            //ставим фокус на равно
            Equal.Focus();
        }
    }
}