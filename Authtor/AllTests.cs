﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cash_register;
using System.Collections.Generic;
using System.Data;
using static Cash_register.SearchFunc;
using static Cash_register.ChekingValidityProduct;
using static Cash_register.SQLRequest;

namespace Tests
{
    [TestClass]
    public class AllTests
    {
        //тест: проверка хеширования
        [TestMethod]
        public void TestHash()
        {
            //#1 ПРАВИЛЬНЫЙ ПАРОЛЬ#
            //для пароля 12314
            string expected = "95751a2e765809e6221e3249319cee73";
            //пароль
            string pass = "12314";
            //хэшируем пароль и записываем в переменную
            string actual = Authorization.Hash(pass);
            //должно совпасть
            Assert.AreEqual(expected, actual);

            //#2 НЕПРАВИЛЬНЫЙ ПАРОЛЬ#
            //хэшированный пароль тот же
            pass = "123zxc";
            actual = Authorization.Hash(pass);
            //должно не совпасть
            Assert.AreNotEqual(expected, actual);
        }

        //тест: проверка правильности получения ID сотрудника при регистрации
        [TestMethod]
        public void TestGiveIdEmployee()
        {
            //#1 ПРАВИЛЬНЫЙ ИНДЕКС#
            //лист со всеми ID сотрудников (предположим)
            List<int> IdAllEmployees = new List<int> { 1, 3, 4, 5, 8, 10, 11, 15, 17, 22 };
            //в одном из окон есть листбокс с сотрудниками, эта переменная - номер выбранного сотруника из списка (0 - начало)
            int index = 6;
            //мы ожидаем, что заходит сотрудник с ID = 11
            int expected = 11;
            //получаем ID из листа
            int actual = Authorization.GiveIdEmployee(IdAllEmployees, index);
            //должно совпасть
            Assert.AreEqual(expected, actual);

            //#2 НЕПРАВИЛЬНЫЙ ИНДЕКС#
            //назначаю неправильный индекс
            index = 2;
            actual = Authorization.GiveIdEmployee(IdAllEmployees, index);
            //должно не совпасть
            Assert.AreNotEqual(expected, actual);
        }

        //тест: правильно ли составляется инфа о пользователе
        [TestMethod]
        public void TestCreatListEmployee()
        {
            //#1 ПРАВИЛЬНЫЙ #
            //создаем лист для ФИО
            List<string> FIO_employee = new List<string>();
            //создаем лист для ID
            List<int> ID_employee = new List<int>();

            //создаем тестовую таблицу
            DataTable employeeFIO = new DataTable("Employees");
            //создаю 1 колонку
            DataColumn column;
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.Int32"),
                ColumnName = "ID"
            };
            //добавил ее
            employeeFIO.Columns.Add(column);
            //создаю 2 колонку
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "LName"
            };
            //добавил ее
            employeeFIO.Columns.Add(column);
            //создаю 3 колонку
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "FName"
            };
            //добавил ее
            employeeFIO.Columns.Add(column);
            //создаю 4 колонку
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "MName"
            };
            //добавил ее
            employeeFIO.Columns.Add(column);
            //создаю 1 строку
            DataRow row;
            row = employeeFIO.NewRow();
            row["ID"] = 1;
            row["LName"] = "Борковский";
            row["FName"] = "Владимир";
            row["MName"] = "Владимирович";
            //добавил ее
            employeeFIO.Rows.Add(row);
            //создаю 2 строку
            row = employeeFIO.NewRow();
            row["ID"] = 2;
            row["LName"] = "Кашлач";
            row["FName"] = "Никита";
            row["MName"] = "Сергеевич";
            //добавил ее
            employeeFIO.Rows.Add(row);

            //создаем строку, которую ожидаем увидеть
            string expectedFIO = "Борковский Владимир Владимирович";
            //ожидаемый ID
            int expectedID = 1;
            //получаем данные
            MainWindow.CreatListEmployee(FIO_employee, employeeFIO, ID_employee);
            //должно совпасть
            Assert.AreEqual(expectedFIO, FIO_employee[0]);
            Assert.AreEqual(expectedID, ID_employee[0]);

            //#2 НЕПРАВИЛЬНЫЙ #
            //должно не совпасть
            Assert.AreNotEqual(expectedFIO, FIO_employee[1]);
            Assert.AreNotEqual(expectedID, ID_employee[1]);
        }

        //тест: правильно ли составляется сумма продаваемых товаров
        [TestMethod]
        public void TestCalculatingAmount()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            //#1 ПРАВИЛЬНЫЙ #
            //создаем лист с товарами (по шаблону того, как реально выглядит строка)
            List<string> ListProductSale = new List<string>
            {
                "Код: 2. Конфеты за кг. Стоимость: 100.12₽ (1 шт)", 
                "Код: 4. Энергетик. Стоимость: 76.4₽ (1 шт)"
            };
            //создаем сумму, которую ошидаем
            double expected = 176.52;
            //получаем актуальную сумму
            double actual = 0;
            actual = Sales1.CalculatingAmount(actual, ListProductSale);
            //должно совпасть
            Assert.AreEqual(expected, actual);

            //#2 НЕПРАВИЛЬНЫЙ #
            expected = 1000;
            //должно не совпасть
            Assert.AreNotEqual(expected, actual);
        }

        //тест: проверяет: в поле строка есть значение? если есть, это число?
        [TestMethod]
        public void TestCountIsOk()
        {
            //#1 ПРАВИЛЬНЫЙ #
            //имитируем строку ввода количества (не должна быть пустая)
            string count = "11";
            //создаем переменную, false - что-то пошло не так, true - все ок (изначально false)
            bool сountIsOk = false;
            //это лист с цифрами для проверки валидации:)
            List<string> Numbers = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            //должно быть true
            Assert.IsTrue(Count.CountIsOk(count, сountIsOk, Numbers));

            //#2 НЕПРАВИЛЬНЫЙ #
            //сначала делаем ошибку - поле пустое
            count = "";
            //должно быть false
            Assert.IsFalse(Count.CountIsOk(count, сountIsOk, Numbers));

            //#3 НЕПРАВИЛЬНЫЙ #
            //теперь ошибка - неверный формат
            count = "1d";
            //должно быть false
            Assert.IsFalse(Count.CountIsOk(count, сountIsOk, Numbers));

            //#4 НЕПРАВИЛЬНЫЙ #
            //теперь ошибка - поле меньше нуля 
            count = "-1";
            //должно быть false
            Assert.IsFalse(Count.CountIsOk(count, сountIsOk, Numbers));
        }

        //тест: проверяет: введенное количество товара есть на складе?
        [TestMethod]
        public void TestCountNotMore()
        {
            //#1 ПРАВИЛЬНЫЙ #
            //bool переменная для проверки (изначально true)
            bool countNotMore = true;
            //имитируем количество в БД
            int countInDB = 67;
            //имитируем вводимое значение
            int countEntered = 2;
            //должно быть true
            Assert.IsTrue(Count.CountNotMore(countNotMore, countInDB, countEntered));

            //#2 НЕПРАВИЛЬНЫЙ #
            //вводимое количество больше имеющегося
            countEntered = 100;
            //должно быть false
            Assert.IsFalse(Count.CountNotMore(countNotMore, countInDB, countEntered));
        }

        //тест: правильно ли работает поиск?
        [TestMethod]
        public void TestSearchProduct()
        {
            //#1 ПРАВИЛЬНЫЙ #
            //создаем искомый товар
            string searchProduct = "ло";
            //создаем лист с товарами
            List<string> Products = new List<string> { "Яблоко", "Энергетик", "Вода 0.5л", "Лосось" };
            //создаем лист с ожидаемыми товарами
            List<string> Expected = new List<string> { "Яблоко", "Лосось" };
            //актульный лист
            List<string> Actual = new List<string>();
            SearchProduct(Products, searchProduct, Actual);
            //должно совпасть
            for(int i = 0; i < Expected.Count; i++)
            {
                Assert.AreEqual(Expected[i], Actual[i]);
            }

            //#2 НЕПРАВИЛЬНЫЙ #
            //создаем лист с ожидаемыми(нет) товарами
            Expected.Clear();
            Expected.Add("Энергетик");
            //должно не совпасть
            for (int i = 0; i < Expected.Count; i++)
            {
                Assert.AreNotEqual(Expected[i], Actual[i]);
            }
        }

        //тест: чек написал цифрой?
        [TestMethod]
        public void TestChequeIsValid()
        {
            //#1 ПРАВИЛЬНЫЙ #
            //создаем искомый чек
            string searchCheque = "2";
            //создаем переменную, которая изменится, если будет цифра (изначально false)
            bool isOk = false;
            //лист с циферками
            List<string> Numbers = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            //должно быть true
            Assert.IsTrue(Refund_of_products.ChequeIsValid(searchCheque, isOk, Numbers));

            //#2 НЕПРАВИЛЬНЫЙ #
            //создаем искомый чек - не цифра
            searchCheque = "2ц";
            //должно быть false
            Assert.IsFalse(Refund_of_products.ChequeIsValid(searchCheque, isOk, Numbers));
        }

        //тест: правильно ли работает поиск по чеку?
        [TestMethod]
        public void TestListOfProductSold()
        {
            //#1 ПРАВИЛЬНЫЙ #
            //создаем лист с товарами по шаблону из приложения
            List<string> ListSoldProducts = new List<string>
            {
                "Номер чека: 2. Конфеты за кг (Цена: 100.12₽). Количество: 1, Сумма: 100.12",
                "Номер чека: 12. Энергетик (Цена: 76.4₽). Количество: 2, Сумма: 152.8"
            };
            //создаем лист для записи после поиска
            List<string> Actual = new List<string>();
            //количество строк в листе товаров
            int count = ListSoldProducts.Count;
            //чек
            string searchCheque = "1";
            //ожидаемый результат (ищет все чеки, где есть такая цифра)
            List<string> Expected = new List<string> { "Номер чека: 12. Энергетик (Цена: 76.4₽). Количество: 2, Сумма: 152.8" };
            //актульный результат
            Refund_of_products.ListOfProductSold(count, searchCheque, ListSoldProducts, Actual);
            //должно совпасть
            for (int i = 0; i < Expected.Count; i++)
            {
                Assert.AreEqual(Expected[i], Actual[i]);
            }

            //#2 НЕПРАВИЛЬНЫЙ #
            //меняем ожидаемый на тот, что не ожидаем
            Expected.Clear();
            Expected.Add("Номер чека: 2. Конфеты за кг (Цена: 100.12₽). Количество: 1, Сумма: 100.12");
            //должно не совпасть
            for (int i = 0; i < Expected.Count; i++)
            {
                Assert.AreNotEqual(Expected[i], Actual[i]);
            }
        }

        //тест: правильно ли высчитывается сумма у проданных товаров?
        [TestMethod]
        public void TestAmountOfSoldProducts()
        {
            //#1 ПРАВИЛЬНЫЙ #
            //переменная для суммы
            double actual = 0;
            //цена товара
            double sale = 150.5;
            //количество товара
            double count = 2;
            //ожидаемая сумма
            double expected = 301;
            //высчитываем актуальную сумму
            actual = Refund_of_products.AmountOfSoldProducts(actual, sale, count);
            //должно совпасть
            Assert.AreEqual(expected, actual);

            //#2 НЕПРАВИЛЬНЫЙ #
            //ожидаемый результат - неверный
            expected = 300;
            //должно не совпасть
            Assert.AreNotEqual(expected, actual);
        }

        //тест: проверка валидации у продуктов при добавлении
        [TestMethod]
        public void TestValidIsOk()
        {
            //#1 ПРАВИЛЬНЫЙ #
            //переменная для количества, которое введено
            string text = "11";
            //переменная bool для проверки
            bool isOkOrNot = false;
            //циферки
            List<string> Numbers = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            //должно быть true
            Assert.IsTrue(ValidIsOk(text, isOkOrNot, Numbers));

            //#2 НЕПРАВИЛЬНЫЙ #
            //переменная для количества, которое введено не правильно
            text = "1d";
            isOkOrNot = false;
            //должно быть false
            Assert.IsFalse(ValidIsOk(text, isOkOrNot, Numbers));

            //#3 ПРАВИЛЬНЫЙ #
            //переменная для цены, которая введена правильно
            text = "115.1";
            isOkOrNot = false;
            //циферки и точка, запятая
            List<string> Signs = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", ".", "," };
            //должно быть true
            Assert.IsTrue(ValidIsOk(text, isOkOrNot, Signs));

            //#4 НЕПРАВИЛЬНЫЙ #
            //переменная для цены, которая введена не правильно
            text = "11/1";
            isOkOrNot = false;
            //должно быть false
            Assert.IsFalse(ValidIsOk(text, isOkOrNot, Signs));
        }

        //тест: проверка доп валидации у цены при добавлении товара
        [TestMethod]
        public void TestPriceValid()
        {
            //#1 ПРАВИЛЬНЫЙ #
            //переменная для цены, которая введена правильно
            string text = "11.1";
            //переменная bool для проверки
            bool isOkOrNot = true;
            //должно быть true
            Assert.IsTrue(PriceValid(text, isOkOrNot));

            //#2 НЕПРАВИЛЬНЫЙ #
            //переменная для цены, которая введена не правильно (две точки)
            text = "11.1.12";
            isOkOrNot = true;
            //должно быть false
            Assert.IsFalse(PriceValid(text, isOkOrNot));

            //#2 НЕПРАВИЛЬНЫЙ #
            //переменная для цены, которая введена не правильно (начинается с точки)
            text = ".11";
            isOkOrNot = true;
            //должно быть false
            Assert.IsFalse(PriceValid(text, isOkOrNot));
        }

        //тест: проверка доп валидации у цены при добавлении товара
        [TestMethod]
        public void TestNameIsOk()
        {
            //#1 ПРАВИЛЬНЫЙ #
            //валидное название товара
            string text = "Колбаса";
            //переменная bool для проверки
            bool nameIsOk = true;
            //должно быть true
            Assert.IsTrue(NameIsOk(text, nameIsOk));

            //#2 НЕПРАВИЛЬНЫЙ #
            //невалидное название товара
            text = "Колбаса?";
            //должно быть false
            Assert.IsFalse(NameIsOk(text, nameIsOk));
        }

        //тест: проверка правильно ли получает дату
        [TestMethod]
        public void TestDate()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            //#1 формат Tuesday, July 7, 2015
            //создаю переменную, чтобы switch понял, что я хочу
            int count = 1;
            //значение, которое я желаю получить
            string expected = "Saturday, December 11, 2021";
            //получаю актуальный вариант
            string actual = Statements.Date(count);
            //сверяем
            Assert.AreEqual(expected, actual);

            //#2 формат 07/20/2015
            count = 2;
            expected = "12/11/2021";
            actual = Statements.Date(count);
            Assert.AreEqual(expected, actual);

            //#3 формат 6:30:25 PM
            count = 3;
            //секунды обрежу, потому что не выйдет
            expected = "11:47 PM";
            actual = Statements.Date(count).Substring(0, 5) + " PM";
            Assert.AreEqual(expected, actual);

            //#4 формат 6:30 PM
            count = 4;
            expected = "11:47 PM";
            actual = Statements.Date(count);
            Assert.AreEqual(expected, actual);

            //#5 формат 07/20/2015 6:30:25 PM
            count = 0;
            //без секунд
            expected = "12/11/2021 11:47 PM";
            actual = Statements.Date(count).Substring(0, 16) + " PM";
            Assert.AreEqual(expected, actual);
        }

        //ТЕСТИРОВАНИЕ ФУНКЦИИ ПОДКЛЮЧЕНИЯ К БД И СОЗДАНИЯ ЗАПРОСОВ
        [TestMethod]
        public void TestSQLrequest()
        {
            //# Протестируем Select
            //предполагаемая строка из БД
            DataTable expected = new DataTable();
            
            //создаю 1 колонку
            DataColumn column;
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.Int32"),
                ColumnName = "PamentId"
            };
            //добавил ее
            expected.Columns.Add(column);
            //создаю 2 колонку
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "TipOfPament"
            };
            //добавил ее
            expected.Columns.Add(column);
            
            //создаю 1 строку
            DataRow row;
            row = expected.NewRow();
            row["PamentId"] = 1;
            row["TipOfPament"] = "Наличные";
            //добавил ее
            expected.Rows.Add(row);
            //актуальная строка из БД
            DataTable actual = SQLrequest("select * from Pament where PamentId = 1");
            //должно совпасть
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                Assert.AreEqual(expected.Rows[i][i], actual.Rows[i][i]);
            }

            //# Протестируем Select
        }
    }
}