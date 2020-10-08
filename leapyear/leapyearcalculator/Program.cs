using System;
using System.IO;

namespace leapyearcalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Räkna ut hur många skottår som passerat mellan två inmatade värden.

            // DateTime.IsLeapYear(year) är en metod man kan använda.

            bool running = true;

            while (running)
            {
                //string input;

                //Console.Write("Write a date: ");
                //input = Console.ReadLine();
                //DateTime startDate = Convert.ToDateTime(input);

                //Console.Write("Write a second date: ");
                //input = Console.ReadLine();
                //DateTime endDate = Convert.ToDateTime(input);

                //int numberOfLeapYears = 0;

                //for (int i = startDate.Year; i < endDate.Year; i++)
                //{
                //    if (DateTime.IsLeapYear(i))
                //    {
                //        numberOfLeapYears++;
                //    }
                //}

                //Console.WriteLine($"Your timespan contains {numberOfLeapYears} leapyears.");

                string input = "";
                DateTime startDate;
                DateTime secondDate;

                Console.Write("Write a date: ");
                input = Console.ReadLine();

                while(!TryParseDate(input, out startDate))
                {
                    Console.Write("Write a date: ");
                    input = Console.ReadLine();
                }

                Console.Write("Write a second date: ");
                input = Console.ReadLine();

                while (!TryParseDate(input, out secondDate))
                {
                    Console.Write("Write a second date: ");
                    input = Console.ReadLine();
                }

                int numberOfLeapYears = 0;

                for (int i = startDate.Year; i <= secondDate.Year; i++)
                {
                    if (DateTime.IsLeapYear(i))
                    {
                        numberOfLeapYears++;
                    }
                }

                Console.WriteLine($"Your timespan contains {numberOfLeapYears} leapyears.");
            }

        }

        public static bool TryParseDate(string input, out DateTime dt)
        {
            try
            {
                dt = Convert.ToDateTime(input);
                return true;
            } catch(Exception e)
            {
                dt = DateTime.Today;
                return false;
            }
        }
    }
}
