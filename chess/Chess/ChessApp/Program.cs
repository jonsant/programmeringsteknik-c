using System;

namespace ChessApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.Write("░");
            //Console.Write("▓");

           

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    int total = i + (j/2);
                    int oddOrEven = total % 2;

                    if (oddOrEven == 0)
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
            Console.ReadKey();
        }
    }
}
