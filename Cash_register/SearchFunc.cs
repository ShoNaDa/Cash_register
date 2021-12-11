using System.Collections.Generic;

namespace Cash_register
{
    public class SearchFunc
    {
        //функция поиска
        public static List<string> SearchProduct(List<string> Products, string searchProduct, List<string> ListOfProducts)
        {
            //ищет любые вхождения подстроки в строках
            foreach (string i in Products)
            {
                if (i.Contains(searchProduct.Trim()) || i.ToLower().Contains(searchProduct.Trim()))
                {
                    ListOfProducts.Add(i);
                }
            }

            return ListOfProducts;
        }
    }
}
