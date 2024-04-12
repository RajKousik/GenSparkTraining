
using System.IO;

namespace ValidityCheck
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string StrNum = Console.ReadLine()??"";
            string RevNum = ReverseString(StrNum);

            int TotalSum= 0;

            for(int i = 0; i < RevNum.Length; i++)
            {
                int Digit = RevNum[i] - 48;
                if (i%2 == 1)
                {
                    Digit *= 2;
                    while(Digit > 10)
                    {
                        Digit = FindSum(Digit);
                    }
                }
                TotalSum += Digit;
            }

            if(TotalSum % 10 == 0)
                Console.WriteLine("Yes...The Given Number is Valid");
            else
                Console.WriteLine("Oops...Not an valid numbere");
        }


        private static string ReverseString(string Str)
        {
            char[] CharArray = Str.ToCharArray();
            Array.Reverse(CharArray);
            return new string(CharArray);
        }

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
