using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для Calculator.xaml
    /// </summary>
    public partial class Calculator : Window
    {
        public Calculator()
        {
            InitializeComponent();
        }

        public void Buttons_of_sings(object sender, RoutedEventArgs e)
        {
            Signs(Convert.ToString(((Button)sender).Content));
        }
        //функция нажатия на кнопки
        public void Buttons_of_numbers(object sender, RoutedEventArgs e)
        {
            Numbers(Convert.ToString(((Button)sender).Content));
        }
        //функция для предотвращения багов с цифрами
        string Numbers(string number)
        {
            if (equation == "")
            {
                equation = number;
                Reply_line.Text = equation;
            }
            else if (equation[equation.Length - 1] == '0' && equation.Length == 1)
            {
                equation = equation.Substring(0, equation.Length - 1);
                equation += number;
                Reply_line.Text = equation;
            }
            else if (equation[equation.Length - 1] == '0' && equation[equation.Length - 2] != '1' && equation[equation.Length - 2] != '2' && equation[equation.Length - 2] != '3' && equation[equation.Length - 2] != '4' && equation[equation.Length - 2] != '5' && equation[equation.Length - 2] != '6' && equation[equation.Length - 2] != '7' && equation[equation.Length - 2] != '8' && equation[equation.Length - 2] != '9' && equation[equation.Length - 2] != '0')
            {
                equation = equation.Substring(0, equation.Length - 1);
                equation += number;
                Reply_line.Text = equation;
            }
            else
            {
                equation += number;
                Reply_line.Text = equation;
            }
            bool comma = Reply_line.Text.Contains(".");
            if (comma == true)
            {
                Reply_line.Text = Reply_line.Text.Replace(".", ",");
            }
            return equation;
        }
        //функция для предотвращения багов со знаками
        string Signs(string sing)
        {
            if (equation == "" || equation == "-")
            {
                if (sing == "-")
                {
                    equation = sing;
                    Reply_line.Text = equation;
                }
                else
                {
                    MessageBox.Show(Convert.ToString("Ошибка!"));
                }
            }
            else if (equation[equation.Length - 1] == '/' || equation[equation.Length - 1] == '*' || equation[equation.Length - 1] == '+' || equation[equation.Length - 1] == '-')
            {
                equation = equation.Substring(0, equation.Length - 1);
                equation += sing;
                Reply_line.Text = equation;
            }
            else
            {
                equation += sing;
                Reply_line.Text = equation;
            }
            bool comma = Reply_line.Text.Contains(".");
            if (comma == true)
            {
                Reply_line.Text = Reply_line.Text.Replace(".", ",");
            }
            return equation;
        }
        //переменная, в которую заполняется выражение
        string equation = "";

        //нажата цифра 0
        private void Click_to_zero(object sender, RoutedEventArgs e)
        {
            if (equation == "" || equation == "0")
            {
                equation = "0";
                Reply_line.Text = equation;
            }
            else if (equation[equation.Length - 1] == '0' && (equation[equation.Length - 2] == '-' || equation[equation.Length - 2] == '*' || equation[equation.Length - 2] == '+'))
            {
                equation = equation.Substring(0, equation.Length - 1);
                equation += "0";
                Reply_line.Text = equation;
            }
            else if (equation[equation.Length - 1] == '/')
            {
                MessageBox.Show(Convert.ToString("Ошибка!"));
            }
            else
            {
                equation += "0";
                Reply_line.Text = equation;
            }
            bool comma = Reply_line.Text.Contains(".");
            if (comma == true)
            {
                Reply_line.Text = Reply_line.Text.Replace(".", ",");
            }
        }
        //нажата кнопка равно
        private void Click_to_equal(object sender, RoutedEventArgs e)
        {
            if (equation[equation.Length - 1] == '/' || equation[equation.Length - 1] == '*' || equation[equation.Length - 1] == '+' || equation[equation.Length - 1] == '-')
            {
                MessageBox.Show(Convert.ToString("Ошибка!"));
            }
            else
            {
                try
                {
                    //Ответ             
                    var result = new DataTable().Compute(equation, null);
                    equation = Convert.ToString(result);
                    Reply_line.Text = Convert.ToString(result);
                    bool comma = equation.Contains(",");
                    if (comma == true)
                    {
                        equation = equation.Replace(",", ".");
                    }
                }
                catch (FormatException message)
                {
                    //сообщение об ошибке если шо
                    MessageBox.Show(Convert.ToString(message));
                }
            }
        }
        //нажата кнопка стереть
        private void Click_to_clear(object sender, RoutedEventArgs e)
        {
            equation = "";
            Reply_line.Text = "0";
        }

        public void Click_back(object sender, RoutedEventArgs e)
        {
            if (MainWindow.IsCashier == true)
            {
                After_login_in_cashier window9 = new After_login_in_cashier();
                window9.Show();
                Close();
            }
            else if (MainWindow.IsCashier == false)
            {
                After_logging_in window2 = new After_logging_in();
                window2.Show();
                Close();
            }
        }
    }
}
