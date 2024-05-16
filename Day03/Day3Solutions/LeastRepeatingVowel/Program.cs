using System.Globalization;

namespace LeastRepeatingVowel
{
    internal class Program
    {

        static string[] SplitWordsByComma(string word)
        {
            string[] words = word.Split(',').Select(w => w.Trim()).ToArray();
            return words;
            
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter a list of words separated by comma (','): ");
            string input = Console.ReadLine()??"";

            string[] words = SplitWordsByComma(input);

            int[] vowelCount = GetVowelCount(words, out int minimumVowelCount);

            Console.WriteLine($"\nThe words with least repeating vowels are: ");
            for (int i = 0; i < vowelCount.Length; i++)
            {
                if (vowelCount[i] == minimumVowelCount)
                {
                    Console.WriteLine(words[i]);
                }
            }
        }

        static int[] GetVowelCount(string[] words, out int minimumVowelCount)
        {
            int[] vowelCount = new int[words.Length];
            minimumVowelCount = int.MaxValue;

            for (int i = 0; i < words.Length; i++)
            {
                vowelCount[i] = CountVowels(words[i]);
                if (vowelCount[i] < minimumVowelCount)
                    minimumVowelCount = vowelCount[i];
            }

            return vowelCount;
        }

        static int CountVowels(string word)
        {
            char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
            int count = 0;

            foreach (char c in word.ToLower())
            {
                if (vowels.Contains(c))
                {
                    count++;
                }
            }

            return count;
        }
    }
}
