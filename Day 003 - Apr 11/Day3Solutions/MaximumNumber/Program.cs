namespace MaximumNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double maxValue = FindMaxValue();
            if (maxValue != double.MinValue)
            {
                Console.WriteLine($"The Maximum Value is {maxValue}");
            }
            else
            {
                Console.WriteLine("Give Valid Inputs");
            }
        }

        static double FindMaxValue()
        {
            double maxValue = double.MinValue;
            bool isStop = false;
            Console.WriteLine("Enter Your Numbers One by one (to stop enter -ve number): ");
            while (!isStop)
            {
                if (double.TryParse(Console.ReadLine(), out double num) != false)
                {
                    if (num < 0)
                    {
                        isStop = true;
                    }
                    else if (num > maxValue)
                    {
                        maxValue = num;
                    }
                }
                else
                {
                    Console.WriteLine("Enter a Valid Number");
                }
            }

            return maxValue;
        }
    }
}
