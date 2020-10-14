using System;

namespace tgif
{
    class Program
    {
        static void Main(string[] args)
        {
            // Skriv en applikation som läser in ett datum via användarinmatning,
            // som sedan räknar ut hur många dagar det är till nästkommande fredag
            // från det inmatade datumet
            // måndag är den första dagen i veckan

            Console.WriteLine("Skriv ett datum: ");
            //string input = Console.ReadLine();

            if (DateTime.TryParse(Console.ReadLine(), out DateTime dateResult))
            {
                int daysCounter = 0;

                while (dateResult.DayOfWeek != DayOfWeek.Friday)
                {
                    dateResult = dateResult.AddDays(1);
                    daysCounter++;
                }

                if (daysCounter == 0)
                {
                    Console.WriteLine("Fredag idag!");
                }
                else
                {
                    Console.WriteLine($"{daysCounter} {(daysCounter > 1 ? "dagar" : "dag")} kvar till fredag!");
                }

            }
            else
            {
                Console.WriteLine("Ej datum?");
            }
            

        }
    }
}
