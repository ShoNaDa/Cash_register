using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using static Cash_register.SQLRequest;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

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

            for (int i = 0; i < dataTables()[0].Rows.Count; i++)
            {
                DataTable dt_FIO_Worker = SQLrequest("Select LName, FName, MName, RoleWorker from Workers where WorkersId = " + dataTables()[1].Rows[i][0]);

                statementOfMonth.Text += "\t\t\t  Смена №" + Convert.ToString(dataTables()[0].Rows[i][0] + "\n" +
                    "  " + Convert.ToString(dt_FIO_Worker.Rows[0][0]) + " " + Convert.ToString(dt_FIO_Worker.Rows[0][1]).Substring(0, 1) +
                    "." + Convert.ToString(dt_FIO_Worker.Rows[0][2]).Substring(0, 1) + ". — " + Convert.ToString(dt_FIO_Worker.Rows[0][3]) + "\n" +
                    "  Денег в начале смены: " + Convert.ToString(dataTables()[2].Rows[i][0]) + "\n" +
                    "  Продажи: " + Convert.ToString(dataTables()[3].Rows[i][0]) + "\n" + 
                    "  Возвраты: " + Convert.ToString(dataTables()[4].Rows[i][0]) + "\n" + 
                    "  Внесения: " + Convert.ToString(dataTables()[5].Rows[i][0]) + "\n" + 
                    "  Изъятия: " + Convert.ToString(dataTables()[6].Rows[i][0]) + "\n" +
                    "  Дата: " + Convert.ToString(dataTables()[7].Rows[i][0]).Split(' ')[0] + "\n" +
                    "\t ИТОГ: " + Convert.ToString(Convert.ToDouble(dataTables()[2].Rows[i][0]) + 
                    Convert.ToDouble(dataTables()[3].Rows[i][0]) - Convert.ToDouble(dataTables()[4].Rows[i][0]) +
                    Convert.ToDouble(dataTables()[5].Rows[i][0]) - Convert.ToDouble(dataTables()[6].Rows[i][0])) + "\n" +
                    "---------------------------------------------------------------------------" + "\n");
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

                for (int i = 0; i < dataTables()[0].Rows.Count; i++)
                {
                    DataTable dt_FIO_Worker = SQLrequest("Select LName, FName, MName, RoleWorker from Workers where WorkersId = " + dataTables()[1].Rows[i][0]);

                    worksheet.Range[$"A{i + 2}"].Value = Convert.ToString(dataTables()[0].Rows[i][0]);
                    worksheet.Range[$"B{i + 2}"].Value = Convert.ToString(dt_FIO_Worker.Rows[0][0]) + " " + Convert.ToString(dt_FIO_Worker.Rows[0][1]).Substring(0, 1) +
                    "." + Convert.ToString(dt_FIO_Worker.Rows[0][2]).Substring(0, 1) + ". — " + Convert.ToString(dt_FIO_Worker.Rows[0][3]);
                    worksheet.Range[$"C{i + 2}"].Value = Convert.ToString(dataTables()[2].Rows[i][0]);
                    worksheet.Range[$"D{i + 2}"].Value = Convert.ToString(dataTables()[3].Rows[i][0]);
                    worksheet.Range[$"E{i + 2}"].Value = Convert.ToString(dataTables()[4].Rows[i][0]);
                    worksheet.Range[$"F{i + 2}"].Value = Convert.ToString(dataTables()[5].Rows[i][0]);
                    worksheet.Range[$"G{i + 2}"].Value = Convert.ToString(dataTables()[6].Rows[i][0]);
                    worksheet.Range[$"H{i + 2}"].Value = Convert.ToString(dataTables()[7].Rows[i][0]).Split(' ')[0];
                    worksheet.Range[$"I{i + 2}"].Value = Convert.ToString(Convert.ToDouble(dataTables()[2].Rows[i][0]) +
                    Convert.ToDouble(dataTables()[3].Rows[i][0]) - Convert.ToDouble(dataTables()[4].Rows[i][0]) +
                    Convert.ToDouble(dataTables()[5].Rows[i][0]) - Convert.ToDouble(dataTables()[6].Rows[i][0]));
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
        public List<DataTable> dataTables()
        {
            List<DataTable> datas = new List<DataTable>();

            string month;
            DateTime date1 = DateTime.Now;
            if (Convert.ToString(date1.ToShortDateString()).Split('/')[0].Length == 1)
            {
                month = "0" + Convert.ToString(date1.ToShortDateString()).Split('/')[0];
            }
            else
            {
                month = Convert.ToString(date1.ToShortDateString()).Split('/')[0];
            }

            DataTable dt_shiftNumber = SQLrequest("Select ShiftNumber from Statements where Month(WorkingDate) = " + month);
            DataTable dt_FK_Worker = SQLrequest("Select FK_Worker from Statements where Month(WorkingDate) = " + month);
            DataTable dt_MoneyAtTheBeginningOfTheShift = SQLrequest("Select MoneyAtTheBeginningOfTheShift from Statements where Month(WorkingDate) = " + month);
            DataTable dt_Sales = SQLrequest("Select Sales from Statements where Month(WorkingDate) = " + month);
            DataTable dt_Refund = SQLrequest("Select Refund from Statements where Month(WorkingDate) = " + month);
            DataTable dt_Deposits = SQLrequest("Select Deposits from Statements where Month(WorkingDate) = " + month);
            DataTable dt_Withdrawals = SQLrequest("Select Withdrawals from Statements where Month(WorkingDate) = " + month);
            DataTable dt_WorkingDate = SQLrequest("Select WorkingDate from Statements where Month(WorkingDate) = " + month);

            datas.Add(dt_shiftNumber);
            datas.Add(dt_FK_Worker);
            datas.Add(dt_MoneyAtTheBeginningOfTheShift);
            datas.Add(dt_Sales);
            datas.Add(dt_Refund);
            datas.Add(dt_Deposits);
            datas.Add(dt_Withdrawals);
            datas.Add(dt_WorkingDate);

            return (datas);
        }
    }
}
