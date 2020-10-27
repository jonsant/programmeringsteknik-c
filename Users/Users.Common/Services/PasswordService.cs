using System;
using System.Linq;
using Users.Common.Models;

namespace Users.Common.Services
{
    public class PasswordService : IPasswordService
    {
        private const string _letters = "abcdefghijklmnopqrstuvwxyz";
        private const string _numbers = "1234567890";
        private const string _special = Constants.Passwords.SpecialCharacters;
        private Random random = new Random();

        public string GeneratePassword(uint length, uint capitalLetters = 1, uint numbers = 1, uint specialChars = 1)
        {
            #region beforeTestProject
            //if (capitalLetters + numbers + specialChars > length || capitalLetters + numbers + specialChars < length)
            //    throw new ArgumentOutOfRangeException("Capital letters, numbers and special characters does not match length of password.");

            //var buffer = new char[length];

            //for (var i = 0; i < buffer.Length; i++)
            //{
            //    char currentCharacter;

            //    if (i < length - numbers - specialChars)
            //    {
            //        currentCharacter = GetRandomCharacter(_letters);

            //        if (i < capitalLetters)
            //            currentCharacter = char.ToUpper(currentCharacter);
            //    }
            //    else if (i <= length - specialChars)
            //    {
            //        currentCharacter = GetRandomCharacter(_numbers);
            //    }
            //    else
            //    {
            //        currentCharacter = GetRandomCharacter(_special);
            //    }

            //    buffer[i] = currentCharacter;
            //}

            //return new string(buffer);

            //StringBuilder sb = new StringBuilder();
            //int capitalLettersCount = (int)capitalLetters;
            //int numbersCount = (int)numbers;
            //int specialCharsCount = (int)specialChars;

            //while (sb.Length < length)
            //{
            //    int rnd = random.Next(3);

            //    switch (rnd)
            //    {
            //        case 0:
            //            if (capitalLettersCount-- > 0)
            //                sb.Append(GetCapitalLetter());
            //            break;
            //        case 1:
            //            if (numbersCount-- > 0)
            //                sb.Append(GetNumber());
            //            break;
            //        case 2:
            //            if (specialCharsCount-- > 0)
            //                sb.Append(GetSpecialChar());
            //            break;
            //        default:
            //            break;
            //    }
            //}

            //return sb.ToString();
            #endregion

            if (capitalLetters + numbers + specialChars > length)
                throw new ArgumentOutOfRangeException("Capital letters, numbers and special characters does not match length.");

            var buffer = new char[length];

            for (var i = 0; i < buffer.Length; i++)
            {
                var currentCharacter = GetRandomCharacter(_letters);

                if (i < capitalLetters)
                {
                    currentCharacter = char.ToUpper(currentCharacter);
                }
                else if (i >= length - specialChars)
                {
                    currentCharacter = GetRandomCharacter(_special);
                }
                else if (i >= length - numbers - specialChars)
                {
                    currentCharacter = GetRandomCharacter(_numbers);
                }

                buffer[i] = currentCharacter;
            }

            return new string(buffer);
        }

        private char GetRandomCharacter(string input)
        {
            return input[random.Next(input.Length)];
        }

        public IServiceResponse ValidatePassword(string password, uint length, uint capitalLetters = 1, uint numbers = 1, uint specialChars = 1)
        {
            ServiceResponse response = new ServiceResponse();

            bool validLength = password.Length >= length;

            int amountOfCapitalLetters = password.Where(letter => _letters.ToUpper().Contains(letter)).Count();
            bool validAmountOfLetters = amountOfCapitalLetters >= capitalLetters;

            int amountOfNumbers = password.Where(letter => _numbers.Contains(letter)).Count();
            bool validAmountOfNumbers = amountOfNumbers >= numbers;

            int amountOfSpecialChars = password.Where(letter => _special.Contains(letter)).Count();
            bool validAmountOfSpecial = amountOfSpecialChars >= specialChars;

            if (validLength && validAmountOfLetters && validAmountOfNumbers && validAmountOfSpecial)
            {
                response.Success = true;
                response.Message = "Password is valid";
            }
            else
            {
                response.Success = false;
                response.Message = "Password is invalid";
            }

            return response;

        }
    }
}
