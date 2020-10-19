using System;
using System.Globalization;

namespace months
{
    class Program
    {
        static void Main(string[] args)
        {
            // Skriv ett program som tar en emot inmatad siffra (1-12)
            // och konverterar siffran till ett månadsnamn på svenska
            // programmet skall kasta ett fel om den inmatade siffran är något annat än 1-12.

            Console.Write("Skriv en siffra (1-12):");
            // CultureInfo culture = SettingsFactory.GetCulture();
            // DateTimeFormatInfo dateFormat = GetDateTimeFormat();
            // TextInfo textFormat = culture.TextInfo;
            // string month = dateFormat.GetMonthName(monthNumber);
            // string monthNameFormatted = textFormat.ToTitleCase(monthName);

            if (int.TryParse(Console.ReadLine(), out int number))
            {
                if (number < 1 || number > 12)
                    throw new ArgumentOutOfRangeException(nameof(number), "value must be, or be between 1 and 12");

                //string month = new DateTime(1, number, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("sv-SE"));
                string month = CultureInfo.GetCultureInfo("sv-SE").DateTimeFormat.GetMonthName(number);

                Console.WriteLine($"{number} = {month}");
                
            }
        }

        //static DateTimeFormatInfo GetDateTimeFormat()
        //{
        //    return SettingsFactory.GetCulture().DateTimeFormat;
        //}
    }
}

            //CultureInfo.DateTimeFormat.GetMonthName;