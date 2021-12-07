using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using static Cash_register.SQLRequest;
using static Cash_register.DateFunc;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Input;
using System.Windows.Controls.Primitives;

namespace Cash_register
{
    /// <summary>
    /// Логика взаимодействия для StatementOfMonth.xaml
    /// </summary>
    public partial class StatementOfMonth : Window
    {
        public StatementOfMonth()
        {
            InitializeComponent();

            for (int i = 0; i < DataTables()[0].Rows.Count; i++)
            {
                DataTable dt_FKWOrkerId = SQLrequest("Select FK_WorkerId from [Shift] where ShiftId = " + DataTables()[0].Rows[i][0]); //получаем ID работника смены
                DataTable dt_FIO_Worker = SQLrequest("Select LName, FName, MName, RoleWorker from Workers where WorkerId = " + dt_FKWOrkerId.Rows[0][0]); //с помощью ID получаем инфу по работнику

                //тернарная операция, проверка если продажи пустые
                DataTable dt_sale = SQLrequest("Select sum(Amount) from Sale where FK_ShiftId = " + DataTables()[0].Rows[i][0]); //получаем сумму продаж за смену
                double sale = dt_sale.Rows[0][0] == DBNull.Value ? 0 : Convert.ToDouble(dt_sale.Rows[0][0]); //если она ничему не равна, то присвоить ноль

                string numberOfShift = DataTables()[0].Rows[i][0].ToString(); //номер смены для чека
                string lName = (string)dt_FIO_Worker.Rows[0][0]; //фамилия работника
                string fName = dt_FIO_Worker.Rows[0][1].ToString().Substring(0, 1); //имя работника
                string mName = dt_FIO_Worker.Rows[0][2].ToString().Substring(0, 1); //отчество работника
                string role = (string)dt_FIO_Worker.Rows[0][3]; //роль работника

                //деньги в начале смены
                DataTable moneyInStartShift = SQLrequest("Select MoneyAtTheBeginningOfTheShift from BalanceAfterCloseCashRegister where BalanceId = " + DataTables()[0].Rows[i][0].ToString());
                
                //если возвраты пустые
                string refund;
                if (Refund_of_products.refunds.ContainsKey(Convert.ToInt32(SQLrequest("Select max(ShiftId) from [Shift]").Rows[0][0]))) 
                {
                    refund = Refund_of_products.refunds[Convert.ToInt32(DataTables()[0].Rows[DataTables()[0].Rows.Count - 1][0])].ToString();
                }
                else
                {
                    refund = "0";
                }

                DataTable depos = SQLrequest("Select Deposits from BalanceAfterCloseCashRegister where BalanceId = " + DataTables()[0].Rows[i][0].ToString()); //взносы смены
                DataTable withdraw = SQLrequest("Select Withdrawals from BalanceAfterCloseCashRegister where BalanceId = " + DataTables()[0].Rows[i][0].ToString()); //изъятия смены
                DataTable date = SQLrequest("Select CloseShiftDate from [Shift] where ShiftId = " + DataTables()[0].Rows[i][0].ToString()); //дата смены

                //итог смены
                double res = Convert.ToDouble(moneyInStartShift.Rows[0][0]) + sale - Convert.ToDouble(refund) 
                    + Convert.ToDouble(depos.Rows[0][0]) - Convert.ToDouble(withdraw.Rows[0][0]);

                //составляем чек
                statementOfMonth.Text += "\t\t\t  Смена №" + numberOfShift + "\n" +
                    "  " + lName + " " + fName +
                    "." + mName + ". — " + role + "\n" +
                    "  Денег в начале смены: " + Convert.ToDouble(moneyInStartShift.Rows[0][0]).ToString() + "\n" +
                    "  Продажи: " + sale.ToString() + "\n" +
                    "  Возвраты: " + refund + "\n" +
                    "  Внесения: " + Convert.ToDouble(depos.Rows[0][0]).ToString() + "\n" +
                    "  Изъятия: " + Convert.ToDouble(withdraw.Rows[0][0]).ToString() + "\n" +
                    "  Дата: " + date.Rows[0][0].ToString().Split(' ')[0] + "\n" +
                    "\t ИТОГ: " +  res.ToString() + "\n" +
                    "---------------------------------------------------------------------------" + "\n";
            }
        }

        private void Click_back(object sender, RoutedEventArgs e)
        {
            Statements statements = new Statements();
            statements.Show();
            Close();
        }
        private void Click_to_save(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = "Отчет за месяц";
            saveFile.Filter = "Excel files |*.xlsx";

            if (saveFile.ShowDialog() == true)
            {
                //создаем приложение
                Excel.Application app = new Excel.Application();
                //создаем книгу
                Excel.Workbook workbook = app.Workbooks.Add();
                //создаем лист
                Excel.Worksheet worksheet = app.Worksheets[1];
                //Создаем заголовки
                worksheet.Name = "Отчет за месяц";
                worksheet.Range["A1"].Value = "Смена №";
                worksheet.Range["B1"].Value = "Работник";
                worksheet.Range["C1"].Value = "Денег в начале смены";
                worksheet.Range["D1"].Value = "Продажи";
                worksheet.Range["E1"].Value = "Возвраты";
                worksheet.Range["F1"].Value = "Внесения";
                worksheet.Range["G1"].Value = "Изъятия";
                worksheet.Range["H1"].Value = "Дата";
                worksheet.Range["I1"].Value = "ИТОГ";

                for (int i = 0; i < DataTables()[0].Rows.Count; i++)
                {
                    DataTable dt_FKWOrkerId = SQLrequest("Select FK_WorkerId from [Shift] where ShiftId = " + DataTables()[0].Rows[i][0]); //получаем ID работника смены
                    DataTable dt_FIO_Worker = SQLrequest("Select LName, FName, MName, RoleWorker from Workers where WorkerId = " + dt_FKWOrkerId.Rows[0][0]); //с помощью ID получаем инфу по работнику

                    //тернарная операция, проверка если продажи пустые
                    DataTable dt_sale = SQLrequest("Select sum(Amount) from Sale where FK_ShiftId = " + DataTables()[0].Rows[i][0]); //получаем сумму продаж за смену
                    double sale = dt_sale.Rows[0][0] == DBNull.Value ? 0 : (double)dt_sale.Rows[0][0]; //если она ничему не равна, то присвоить ноль

                    //если возвраты пустые
                    string refund;
                    if (Refund_of_products.refunds.ContainsKey(Convert.ToInt32(SQLrequest("Select max(ShiftId) from [Shift]").Rows[0][0])))
                    {
                        refund = Refund_of_products.refunds[Convert.ToInt32(DataTables()[0].Rows[i][0])].ToString();
                    }
                    else
                    {
                        refund = "0";
                    }

                    //заносим в excel данные в ячейки
                    worksheet.Range[$"A{i + 2}"].Value = DataTables()[0].Rows[i][0].ToString();
                    worksheet.Range[$"B{i + 2}"].Value = (string)dt_FIO_Worker.Rows[0][0] + " " + dt_FIO_Worker.Rows[0][1].ToString().Substring(0, 1) +
                    "." + dt_FIO_Worker.Rows[0][2].ToString().Substring(0, 1) + ". — " + (string)dt_FIO_Worker.Rows[0][3];
                    worksheet.Range[$"C{i + 2}"].Value = SQLrequest("Select MoneyAtTheBeginningOfTheShift from BalanceAfterCloseCashRegister where BalanceId = " + DataTables()[0].Rows[i][0].ToString()).Rows[0][0].ToString();
                    worksheet.Range[$"D{i + 2}"].Value = sale.ToString();
                    worksheet.Range[$"E{i + 2}"].Value = refund;
                    worksheet.Range[$"F{i + 2}"].Value = SQLrequest("Select Deposits from BalanceAfterCloseCashRegister where BalanceId = " + DataTables()[0].Rows[i][0].ToString()).Rows[0][0].ToString();
                    worksheet.Range[$"G{i + 2}"].Value = SQLrequest("Select Withdrawals from BalanceAfterCloseCashRegister where BalanceId = " + DataTables()[0].Rows[i][0].ToString()).Rows[0][0].ToString();
                    worksheet.Range[$"H{i + 2}"].Value = SQLrequest("Select CloseShiftDate from [Shift] where ShiftId = " + DataTables()[0].Rows[i][0].ToString()).Rows[0][0].ToString().Split(' ')[0];
                    worksheet.Range[$"I{i + 2}"].Value = (Convert.ToDouble(SQLrequest("Select MoneyAtTheBeginningOfTheShift from BalanceAfterCloseCashRegister where BalanceId = " + DataTables()[0].Rows[i][0].ToString()).Rows[0][0]) +
                        sale - Convert.ToDouble(refund) +
                        Convert.ToDouble(SQLrequest("Select Deposits from BalanceAfterCloseCashRegister where BalanceId = " + DataTables()[0].Rows[i][0].ToString()).Rows[0][0]) -
                        Convert.ToDouble(SQLrequest("Select Withdrawals from BalanceAfterCloseCashRegister where BalanceId = " + DataTables()[0].Rows[i][0].ToString()).Rows[0][0])).ToString();
                }
                //ставим ширину по дефолту в соответствии с содержимым
                for (int i = 1; i < 10; i++)
                {
                    worksheet.Columns[i].AutoFit();
                }
                //ставим выравнивание заголовков по центру и жирным шрифтом
                worksheet.get_Range("A1", "I1").Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                worksheet.get_Range("A1", "I1").Cells.Font.Bold = true;

                workbook.SaveAs(saveFile.FileName);
                workbook.Close();
                app.Quit();
            }
        }
        //функция с получением данных из бд в виде листа
        public List<DataTable> DataTables()
        {
            List<DataTable> datas = new List<DataTable>();

            //инфа за текущий месяц
            DataTable dt_shiftNumber = SQLrequest("Select * from [Shift] where Month(CloseShiftDate) = " + DateFunction()[0]);
            
            datas.Add(dt_shiftNumber);

            return datas;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Button_back.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            if (e.Key == Key.Enter)
            {
                Button_save.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
    }
}
