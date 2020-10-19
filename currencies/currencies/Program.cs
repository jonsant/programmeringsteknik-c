using currencies.Models;
using currencies.Services;
using System;

namespace currencies
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Skriv ett program som läser in en fil med växlingskurser och
            // sedan konverterar en inmatad valuta till svenska kronor.

            // Exempelvis: 100 USD, eller 50 GBP

            // Exempelvis ut: 900 SEK


            // 2. Skapa sedan ett uppslagsverk med valutanamn och skriv ut namnen på valutorna konverteringen sker emellan.
            // (Valutor lagras på RegionInfo, en egenskap på CultureInfo) 

            // 3. Lägg till ett ytterligare val för valuta att konvertera till (förutom SEK).

            // Exempelvis: 100 USD -> GBP eller AUD

            string targetCurrency = "SEK";

            string path = "Resources\\Riksbanken_2020-10-13.csv";

            MoneyConverter moneyConverter = new MoneyConverter(path, targetCurrency);

            Console.WriteLine("Skriv in önskad växlings-valuta och mängd (t.ex. 100 USD)");
            string input = Console.ReadLine();

            Console.WriteLine("Skriv vilken valuta du vill växla till (t.ex. GBP)");
            string currencyInput = Console.ReadLine();

            Money enteredMoney = MoneyParser.Parse(input);
            Money convertedMoney = moneyConverter.ConvertToTargetCurrency(enteredMoney);

            if (currencyInput != targetCurrency)
            {
                convertedMoney = moneyConverter.ConvertFromTargetCurrency(convertedMoney.Amount, currencyInput);
            }


            Console.WriteLine($"Dina {enteredMoney} ({GetCurrencyName(enteredMoney)} blir {convertedMoney} ({GetCurrencyName(convertedMoney)})");




            //string path = ".\\Resources\\Riksbanken_2020-10-13.csv";

            //Console.Write("Write a number and currency: ");
            //string[] input = Console.ReadLine().Split(' ');
            //decimal inputNumber = Convert.ToDecimal(input[0]);
            //string inputCurrency = input[1];

            //using (Stream fileStream = File.OpenRead(path))
            //{
            //    using (StreamReader reader = new StreamReader(fileStream))
            //    {
            //        string header = reader.ReadLine();

            //        while (!reader.EndOfStream)
            //        {
            //            string[] line = reader.ReadLine().Split(';');

            //            string date = line[0];

            //            string[] foreignCurrencyAndNumber = line[1].Split(' ');
            //            decimal foreignNumber = Convert.ToDecimal(foreignCurrencyAndNumber[0]);
            //            string foreignCurrency = foreignCurrencyAndNumber[1];

            //            decimal sweFactor = Convert.ToDecimal(line[2], CultureInfo.InvariantCulture);

            //            if (inputCurrency == foreignCurrency)
            //            {

            //                decimal converted = inputNumber * (foreignNumber == 100 ? (sweFactor / 100) : sweFactor);
            //                Console.WriteLine(converted);
            //            }

            //        }

            //    }
            //}

        }

        public static string GetCurrencyName(Money money)
        {
            return CurrencyLookup.GetCurrencyName(money.Currency);
        }
    }
}
