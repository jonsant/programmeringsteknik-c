using System;
using System.Collections.Generic;
using System.Threading;

namespace WordsApp
{
    class Program
    {
        static void Main(string[] args)
        { 
            // Word count
            // Vowel count
            // Longest word

            Console.WriteLine("Write something: ");
            string text = Console.ReadLine();

            var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u', 'y', 'å', 'ä', 'ö' };
            int vowelCount = 0;

            // "Count" words
            int wordCount = text.Split(' ').Length;

            // Count vowels
            for (int i = 0; i < text.Length; i++)
            {
                if (vowels.Contains(text.ToLower()[i]))
                {
                    vowelCount++;
                }
            }

            // Find longest word
            string[] words = text.Split(new[] { " " }, StringSplitOptions.None);
            string word = "";
            int counter = 0;
            foreach (String s in words)
            {
                if (s.Length > counter)
                {
                    word = s;
                    counter = s.Length;
                }
            }

            Console.WriteLine($"Word count: {wordCount}\n" +
                $"Vowel count: {vowelCount}\n" +
                $"Longest word: {word}");

            Console.ReadKey();
        }
    }
}
