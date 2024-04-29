
using System.Collections.Generic;

namespace LeetcodeApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();

            
            bool isExit = false;
            while (isExit == false)
            {
                program.DisplayMenu();
                string? choice = Console.ReadLine();
                switch (choice)
                {

                    case "1":
                        GetMinimumDepthOfBinaryTree();
                        break;
                    case "2":
                        GetExcelSheetTitle();
                        break;
                    case "3":
                        CheckLinkedListCycle();
                        break;
                    case "4":
                        isExit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice, try again");
                        break;
                }
            }
        }

        private static async void CheckLinkedListCycle()
        {
            MyLinkedList list = new MyLinkedList();
            ListNode head = list.createLinkestList();

            bool isCycle = await list.hasCycle(head);

            if (isCycle)
            {
                Console.WriteLine();
                Console.WriteLine("** ALERT **");
                Console.WriteLine("The Linked list has cycle");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("No cycle found.");
                Console.WriteLine("Here is the linked list: ");
                list.PrintLinkedList(head);
            }
        }

        private async static void GetExcelSheetTitle()
        {
            Console.WriteLine("Enter a Column Number: ");
            int result;
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("Invalid input, please try again...");
                Console.WriteLine("\nEnter a Column Number: ");
            }

            ExcelColumnTitle excelColumnTitle = new ExcelColumnTitle();
            string columnTitle = await excelColumnTitle.convertToTitle(result);
            Console.WriteLine("The Column Title of {0} is {1}", result, columnTitle);
        }

        private async static void GetMinimumDepthOfBinaryTree()
        {
            TreeBuilder treeBuilder = new TreeBuilder();

            List<int> treeValues = new List<int>();
            string strNum;
            while (true)
            {
                Console.WriteLine("Enter values one by one (Enter -1 for null, Type stop, if you want to exit): ");
                strNum = Console.ReadLine();
                if(strNum.ToUpper() == "STOP")
                {
                    break;
                }
                int n;
                while (!int.TryParse(strNum, out n))
                {
                    Console.WriteLine("Invalid input, please try again...");
                    Console.WriteLine("Enter values one by one (Enter -1 for null, Type stop if you want to exit): ");
                }
                treeValues.Add(n);
            }

            int[] values = new int[] { 2, -1, 3, -1, 4, -1, 5, -1, 6 };
            TreeNode rootNode = treeBuilder.BuildTree(treeValues);

            int minDepth = await treeBuilder.minDepth(rootNode);
            Console.WriteLine("Minimum Depth of the tree is {0}", minDepth);

        }

        private void DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("-------LeetCode Solutions-------");
            
            Console.WriteLine("1. Minimum Depth of binary tree");
            Console.WriteLine("2. Column Title of Excel Sheet");
            Console.WriteLine("3. Linked List Cycle");
            Console.WriteLine("4. Exit");
            Console.WriteLine("Enter your choice: ");
            
            
        }
    }
}
