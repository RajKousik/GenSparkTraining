namespace ShoppingApplication
{
    internal class Program
    {
        public delegate T calcDel<T>(T n1, T n2);//creating a type that refferes to a method
        //void Calculate(calcDel<int> cal)
        //{
        //    int n1 = 20, n2 = 10;
        //    int result = cal(n1, n2);
        //    Console.WriteLine($"The result of {n1} and {n2} is {result}");
        //}
        
        void Calculate(Func<int, int, int> cal)
        {
            int n1 = 20, n2 = 10;
            int result = cal(n1, n2);
            Console.WriteLine($"The result of {n1} and {n2} is {result}");
        }
        public int Add(int num1, int num2)
        {
            return (num1 + num2);
        }

        public int Sub(int num1, int num2)
        { 
            return (num1 - num2);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Program program = new Program();

            //calcDel c1 = new calcDel(program.Add);
            //calcDel<int> c1 = new calcDel<int>(program.Add);
            //calcDel<int> c2 = delegate (int num1, int num2)
            //{
            //    return (num1 - num2);
            //};
            //calcDel<int> c3 = (int num1, int num2)=> (num1 * num2);
            //program.Calculate(c1);
            //program.Calculate(c2);
            //program.Calculate(c3);

            //Func<int, int, int> c1 = (num1, num2) => (num1 + num2);
            //program.Calculate(c1);


            //int[] numbers = { 40, 56, 23, 54, 12, 32 };

            //var anotherArray = from n in numbers where n > 30 select n;
            //var another = numbers.Where((n) => n > 30);


            //foreach (var item in anotherArray)
            //{
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine("------");
            //foreach (var item in another)
            //{
            //    Console.WriteLine(item);
            //}


        }
    }
}
