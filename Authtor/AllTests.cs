using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cash_register;

namespace Tests
{
    [TestClass]
    public class AllTests
    {
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


    }
}
