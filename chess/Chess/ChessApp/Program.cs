using System;

namespace ChessApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Rita ett schackbräde med hjälp av dessa två tecken ░ ▓.
            // Använd gärna metoder för att lösa problemet.
            // Man behöver använda % (modulo)

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        Console.Write("▓");
                    }
                    else
                    {
                        Console.Write("░");
                    }
                }
                Console.WriteLine();
            }

            //for (int i = 0; i < 8; i++)
            //{
            //    for (int j = 0; j < 16; j++)
            //    {
            //        int total = i + (j / 2);
            //        int oddoreven = total % 2;

            //        if (oddoreven == 0)
            //        {
            //            Console.write("▓");
            //        }
            //        else
            //        {
            //            Console.write("░");
            //        }
            //    }

            //    Console.writeline();
            //}
            Console.ReadKey();
        }
    }
}