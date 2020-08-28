using System;

namespace flag
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.Write("░");
            //Console.Write("▓");

            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 20; col++)
                {
                    Console.Write("j");
                }

                Console.WriteLine();
            }
        }
    }
}
