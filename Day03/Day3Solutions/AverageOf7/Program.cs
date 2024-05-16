namespace AverageOf7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double avgValue = FindAvgValue();
            if (avgValue != double.MinValue)
            {
                Console.WriteLine($"The Average of numbers divided by 7 is {avgValue}");
            }
            else
            {
                Console.WriteLine("Give Valid Inputs");
            }
        }

        static double FindAvgValue()
        {
            double sum = 0;
            int totalNum = 0;
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
                    else if (num % 7 == 0)
                    {
                        sum += num;
                        totalNum++;
                    }
                }
                else
                {
                    Console.WriteLine("Enter a Valid Number");
                }
            }
            return totalNum != 0 ? sum / totalNum : 0;
        }
    }
}
