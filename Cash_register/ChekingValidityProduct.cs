using System;
using System.Collections.Generic;

namespace Cash_register
{
    public class ChekingValidityProduct
    {
        //функция для проверки валидации цены и количества
        public static bool ValidIsOk(string text, bool isOkOrNot, List<string> elems)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (isOkOrNot)
                {
                    isOkOrNot = false;
                }
                for (int j = 0; j < elems.Count; j++)
                {
                    if (Convert.ToString(text[i]).Contains(elems[j]))
                    {
                        isOkOrNot = true;
                        break;
                    }
                }
                if (!isOkOrNot)
                {
                    break;
                }
            }

            return isOkOrNot;
        }

        //дополнительная функция проверки валидации для цены
        public static bool PriceValid(string text, bool priceIsOk)
        {
            int count = 0;

            //проверяем чтобы не было 2-ух запятых
            for (int i = 0; i < text.Length; i++)
            {
                if (Convert.ToString(text[i]) == "." || Convert.ToString(text[i]) == ",")
                {
                    count++;
                    if (count == 2)
                    {
                        priceIsOk = false;
                        break;
                    }
                }
            }

            //проверяем, что цена не начиналась и не кончалась ',' и '.'
            if (priceIsOk)
            {
                if (Convert.ToString(text[0]) == "." || Convert.ToString(text[0]) == "," ||
                    Convert.ToString(text[text.Length - 1]) == "." || Convert.ToString(text[text.Length - 1]) == ",")
                {
                    priceIsOk = false;
                }
            }

            return priceIsOk;
        }

        //функция проверки валидности названия
        public static bool NameIsOk(string text, bool nameIsOk)
        {
            //проверяем правильно ли написано название
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '-' || text[i] == '(' || text[i] == '?')
                {
                    nameIsOk = false;
                }
            }

            return nameIsOk;
        }
    }
}
