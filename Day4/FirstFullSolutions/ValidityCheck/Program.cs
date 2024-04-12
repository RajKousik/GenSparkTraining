
using System.IO;

namespace ValidityCheck
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.Write("Enter a Number: ");
            string StrNum = Console.ReadLine()??"";
            string RevNum = ReverseString(StrNum);

            int TotalSum= 0;

            for(int i = 0; i < RevNum.Length; i++)
            {
                int Digit = RevNum[i] - '0';
                if (i%2 == 1)
                {
                    Digit *= 2;
                    while(Digit > 9)
                    {
                        Digit = FindSum(Digit);
                        //Digit -= 9; Even this works exactly as same as FindSum Function
                    }
                }
                TotalSum += Digit;
            }

            if(ValidityCheck(TotalSum))
                Console.WriteLine("Yes...The Given Number is Valid");
            else
                Console.WriteLine("Oops...Not an valid numbere");
        }

        private static bool ValidityCheck(int Number)
        {
            return (Number % 10 == 0);
        }

        /* 
          maximum possible value if we multiply by 2 is 18
            18 - 9 = 9
            17 - 9 = 8
            16 - 9 = 7
            15 - 9 = 6
            14 - 9 = 5
            13 - 9 = 4
            12 - 9 = 3
            11 - 9 = 3
            10 - 9 = 1
            
            So subtracting 9 from the number will give the sum of digits
         */


        /// <summary>
        /// Function reverses a given string
        /// </summary>
        /// <param name="Str">String to be reversed</param>
        /// <returns>Reversed string</returns>
        private static string ReverseString(string Str)
        {
            char[] CharArray = Str.ToCharArray();
            Array.Reverse(CharArray);
            return new string(CharArray);
        }

        /// <summary>
        /// Find sum of all the digits in a given number
        /// </summary>
        /// <param name="Num">A Number whose sum to be find</param>
        /// <returns>Returns the sum of all the digits in a number</returns>

        private static int FindSum(int Num)
        {
            int SumOfDigits = 0;
            while(Num > 0 )
            {
                SumOfDigits += (Num % 10);
                Num /= 10;
            }
            return SumOfDigits;
        }
    }
}
