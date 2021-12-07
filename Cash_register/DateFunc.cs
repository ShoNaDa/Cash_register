using System;
using System.Collections.Generic;

namespace Cash_register
{
    class DateFunc
    {
        public static List<string> DateFunction()
        {
            List<string> getDate = new List<string>();
            getDate.Clear();

            DateTime date1 = DateTime.Now;
            string month, day, year;

            year = Convert.ToString(date1.ToShortDateString()).Split('/')[2];

            if (Convert.ToString(date1.ToShortDateString()).Split('/')[0].Length == 1)
            {
                month = "0" + Convert.ToString(date1.ToShortDateString()).Split('/')[0];
            }
            else
            {
                month = Convert.ToString(date1.ToShortDateString()).Split('/')[0];
            }
            if (Convert.ToString(date1.ToShortDateString()).Split('/')[1].Length == 1)
            {
                day = "0" + Convert.ToString(date1.ToShortDateString()).Split('/')[1];
            }
            else
            {
                day = Convert.ToString(date1.ToShortDateString()).Split('/')[1];
            }

            getDate.Add(month);
            getDate.Add(day);
            getDate.Add(year);

            return getDate;
        }

    }
}
