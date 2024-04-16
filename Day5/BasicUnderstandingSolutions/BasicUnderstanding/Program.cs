namespace BasicUnderstanding
{
    internal class Program
    {
        private void SequentialStatements()
        {
            Console.WriteLine("\n\n\nSEQUENTIAL STATEMENTS");
            Console.WriteLine("Hello");
            Console.WriteLine("Hi");
            int num1 = 40;
            int num2 = 20;
            int num3 = num1 / num2;
            Console.WriteLine($"{num1}/{num2} is {num3}");
        }

        private void SelectionStatements()
        {
            Console.WriteLine("\n\n\nSELECTION STATEMENTS");
            Console.Write("\n");
            Console.WriteLine("Enter a Number: ");
            int number = Convert.ToInt32(Console.ReadLine());

            if (number == 1)
            {
                Console.WriteLine("You entered 1");
            }
            else if (number == 2)
            {
                Console.WriteLine("You entered 2");
            }
            else if (number == 3)
            {
                Console.WriteLine("You entered 3");
            }
            else
            {
                Console.WriteLine("Sorry, we can't track you!");
            }
        }

        void SwitchStatements()
        {
            Console.WriteLine("\n\n\nSWITCH STATEMENTS");
            int ChoiceSelected = 3;
            switch (ChoiceSelected)
            {
                case 1:
                    Console.WriteLine("The selected choice is 1");
                    break;
                case 2:
                    Console.WriteLine("The selected choice is 2");
                    break;
                case 3:
                    Console.WriteLine("The selected choice is 3");
                    break;
                default:
                    Console.WriteLine("Default Case");
                    break;
            }
        }


        void IterativeForLoopStatement()
        {
            Console.WriteLine("\n\n\nFOR LOOP");
            int SIZE = 5;
            for (int i = 0; i < SIZE; i++)
            {
                Console.WriteLine(i);
            }
        }

        void IterativeWhileLoopStatement()
        {
            Console.WriteLine("\n\n\nWHILE LOOP");
            int SIZE = 5;
            int i = 0;
            while (i < SIZE)
            {
                if (i == 3)
                {
                    i++;
                    continue;
                }
                Console.WriteLine(i);
                i++;

            }
        }

        void IterativeDoWhileLoopStatement()
        {
            Console.WriteLine("\n\n\nWHILE LOOP");
            int i = -1;
            do
            {
                Console.WriteLine(i);
                i++;
            } while (i >= 0 && i < 100);
        }


        private void FindRepeatedDigitsInThreeDigitNumbers()
        {
            int[] numbers = { 777, 315, 474, 666, 233, 641, 533, 315 };
            int CountOfRepeatingNumbers = 0;
            Console.WriteLine("The repeated digit numbers are: ");
            for (int i = 0; i < numbers.Length; i++)
            {
                int firstDigit = numbers[i] % 10;
                int secondDigit = (numbers[i] / 10) % 10;
                int thirdDigit = numbers[i] / 100;

                if (firstDigit == secondDigit && secondDigit == thirdDigit)
                {
                    CountOfRepeatingNumbers++;
                    Console.WriteLine(numbers[i]);
                }
            }
            Console.WriteLine($"The total count is {CountOfRepeatingNumbers}.");


            //for (int i = 0; i < numbers.Length; i++)
            //{
            //    if (numbers[i] % 111 == 0)
            //    {
            //        CountOfRepeatingNumbers++;
            //        Console.WriteLine(numbers[i]);
            //    }
            //}
            //Console.WriteLine($"The total count is {CountOfRepeatingNumbers}.");



        }

        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            Program program = new Program();
            //program.SequentialStatements();
            //program.SelectionStatements();
            //program.SwitchStatements();
            //program.IterativeForLoopStatement();
            //program.IterativeWhileLoopStatement();
            //program.IterativeDoWhileLoopStatement();

            program.FindRepeatedDigitsInThreeDigitNumbers();

        }
    }
}
