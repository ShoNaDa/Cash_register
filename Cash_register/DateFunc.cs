using System;
using System.Collections.Generic;

namespace Cash_register
{
    public class DateFunc
    {
        //функция для изменения формата получаемой даты, для занесения новой даты в БД
        public static List<string> DateFunction()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            List<string> getDate = new List<string>();

            getDate.Clear();

            DateTime date1 = DateTime.Now;
            string month, day, year;

            //из полученной даты вытаскиваем год
            year = Convert.ToString(date1.ToShortDateString()).Split('/')[2];

            //теперь месяц (и если месяц, например, 7, то добавить ноль - 07)
            if (Convert.ToString(date1.ToShortDateString()).Split('/')[0].Length == 1)
            {
                month = "0" + Convert.ToString(date1.ToShortDateString()).Split('/')[0];
            }
            else
            {
                month = Convert.ToString(date1.ToShortDateString()).Split('/')[0];
            }
            //тоже самое с днем
            if (Convert.ToString(date1.ToShortDateString()).Split('/')[1].Length == 1)
            {
                day = "0" + Convert.ToString(date1.ToShortDateString()).Split('/')[1];
            }
            else
            {
                day = Convert.ToString(date1.ToShortDateString()).Split('/')[1];
            }

            //записываем дату в лист
            getDate.Add(month);
            getDate.Add(day);
            getDate.Add(year);

            return getDate;
        }

    }
}
