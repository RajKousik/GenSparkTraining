namespace MathsOperations
{
    internal class Program
    {
        static double Add(double iNum1, double iNum2)
        {
            return (iNum1 + iNum2);
        }

        static double Product(double iNum1, double iNum2)
        {
            return (iNum1 * iNum2);
        }

        static double Divide(double iNum1, double iNum2)
        {
            return iNum1 / iNum2;
        }

        static double Remainder(double iNum1, double iNum2)
        {
            return iNum1 % iNum2;
        }

        static double Difference(double iNum1, double iNum2)
        {
            return iNum2 - iNum1;
        }

        static double GetNumber()
        {
            double iNum;
            Console.WriteLine("Enter a number:");
            while (double.TryParse(Console.ReadLine(), out iNum) == false)
                Console.WriteLine("Invalid entry. Please enter a number");
            return iNum;
        }

        static void DisplayResult(double result, string op)
        {
            Console.WriteLine($"The {op} is {result}");
        }

        static void Calculate()
        {
            double iNum1, iNum2;
            iNum1 = GetNumber();
            iNum2 = GetNumber();

            DisplayResult(Add(iNum1, iNum2), "sum");
            DisplayResult(Difference(iNum1, iNum2), "difference");
            DisplayResult(Product(iNum1, iNum2), "product");
            DisplayResult(Divide(iNum1, iNum2), "quotient on dividing number 1 by number 2");
            DisplayResult(Remainder(iNum1, iNum2), "remainder on dividing number 1 by number 2");
        }
        static void Main(string[] args)
        {
            Calculate();
        }


    }
}
