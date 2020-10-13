using System;
using System.Collections.Generic;
using System.Text;

namespace vowelapp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Skriv en konsolapplikation som tar bort vokaler (konstigt va?) från en inmatad sträng.
            // Applikationen skall både presentera den resulterande strängen och antalet vokaler som togs bort.

            HashSet<char> vowels = new HashSet<char>{ 'a', 'e', 'i', 'o', 'u', 'y', 'å', 'ä', 'ö'};
            StringBuilder newString = new StringBuilder();
            int vowelCount = 0;

            Console.Write("Skriv något: ");
            string input = Console.ReadLine();

            foreach (var letter in input.ToLower())
            {
                if (!vowels.Contains(letter))
                {
                    newString.Append(letter);
                    continue;
                }
                vowelCount++;
            }

            Console.WriteLine($"Result: {newString}\nRemoved {vowelCount} vowels.");
        }
    }
}
