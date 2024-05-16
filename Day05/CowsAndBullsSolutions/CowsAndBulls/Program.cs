namespace CowsAndBulls
{
    internal class Program
    {
        /// <summary>
        /// Get the users guess via console
        /// </summary>
        /// <returns></returns>
        static string GetGuess()
        {
            string guess;
            Console.WriteLine("Enter your guess:");
            guess = Console.ReadLine() ?? "";

            while (guess == null || guess.Length != 4)
            {
                Console.WriteLine("Invalid Input!! Enter a 4 letter word!!");
                guess = Console.ReadLine() ?? "";
            }
            return guess;
        }

        /// <summary>
        /// Checks if the letters of the guess match the letters of the word exactly
        /// </summary>
        /// <param name="guess">User's guess</param>
        /// <param name="word">Answer to be found</param>
        /// <returns></returns>
        static int CheckCows(string guess, string word)
        {
            int cows = 0;
            for (int i = 0; i < word.Length; i++)
            {
                if (guess[i] == word[i])
                {
                    cows++;
                }
            }
            return cows;
        }

        /// <summary>
        /// Checks whether the users guess contains letters from the answer
        /// </summary>
        /// <param name="guess">User's guess</param>
        /// <param name="word">Answer to be found</param>
        /// <returns></returns>
        static int CheckBulls(string guess, string word)
        {
            int bulls = 0;
            for (int i = 0; i < word.Length; i++)
            {
                if (word.Contains(guess[i]) && guess[i] != word[i])
                {
                    bulls++;
                }
            }
            return bulls;
        }
        static void Main(string[] args)
        {
            int cows = 0, bulls = 0;
            string ans = "game";
            string guess;
            do
            {
                guess = GetGuess();
                cows = CheckCows(guess, ans);
                bulls = CheckBulls(guess, ans);
                Console.WriteLine($"Cows - {cows}\tBulls - {bulls}");
            } while (cows != 4);

            Console.WriteLine("Congrats!!! You win!!!");
        }
    }
}