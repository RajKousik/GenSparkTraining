namespace NameLength
{
    internal class Program
    {
        static string GetName()
        {
            string? name;
            Console.WriteLine("Enter a string:");
            

            while(true)
            {
                name = Console.ReadLine();
                if (name == "")
                    Console.WriteLine("Enter a valid name");
                else 
                    break;
            }

            return name;
        }


        static int LengthOfName(string name)
        {
            return (name==null || name=="") ? -1 : name.Length;
        }
        static void Main(string[] args)
        {
            string name = GetName();
            int len = LengthOfName(name);

            if(len != -1)
                Console.WriteLine($"The length is {len}");
            else
                Console.WriteLine("The name entered is invalid!");
        }

    }
}
