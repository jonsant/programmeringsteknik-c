using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Users.Common.Services;
using Users.Common;

namespace Pwd.Tests
{
    [TestClass]
    public class PwdTests
    {
        [TestMethod]
        public void password_is_of_length()
        {
            var passwordService = new PasswordService();
            string password = passwordService.GeneratePassword(8);

            Assert.AreEqual(8, password.Length);
        }

        [TestMethod]
        public void password_mutates()
        {
            var passwordService = new PasswordService();
            string firstPassword = passwordService.GeneratePassword(6);
            string secondPassword = passwordService.GeneratePassword(6);

            Assert.AreNotEqual(firstPassword, secondPassword);
        }

        [TestMethod]
        public void too_many_parameters_throws_error()
        {
            var passwordService = new PasswordService();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => passwordService.GeneratePassword(3, 2, 2, 2));
        }

        [TestMethod]
        public void correct_amount_of_capital_letters()
        {
            var passwordService = new PasswordService();
            var password = passwordService.GeneratePassword(5, 2);

            var capitalLetterCount = password.Count(x => char.IsUpper(x));

            Assert.AreEqual(2, capitalLetterCount);
        }

        [TestMethod]
        public void correct_amount_of_numbers()
        {
            var passwordService = new PasswordService();
            var password = passwordService.GeneratePassword(5, 1, 2);

            var numbersCount = password.Count(x => char.IsDigit(x));

            Assert.AreEqual(2, numbersCount);
        }

        [TestMethod]
        public void correct_amount_of_special_characters()
        {
            var passwordService = new PasswordService();
            var password = passwordService.GeneratePassword(8, 1, 1, 3);

            var specialChars = Constants.Passwords.SpecialCharacters;

            //var specialCount = password.Count(x => char.IsSymbol(x));
            var specialCount = password.Count(x => specialChars.Contains(x));

            Assert.AreEqual(3, specialCount);
        }
    }
}
