using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace LeetcodeApplication
{
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x)
        {
            val = x;
            next = null;
        }
    }
    public class MyLinkedList
    {

        public ListNode createLinkestList()
        {
            ListNode head, tail;

            Console.WriteLine("Enter total no of elements: ");
            int n;
            while (!int.TryParse(Console.ReadLine(), out n))
            {
                Console.WriteLine("Invalid input, please try again...");
                Console.WriteLine("\nEnter a Number: ");
            }
            int num;
            Console.WriteLine("\nEnter the numbers one by one: ");
            while (!int.TryParse(Console.ReadLine(), out num))
            {
                Console.WriteLine("Invalid input, please try adding again...");
            }

            head = new ListNode(num);
            tail = head;

            for (int i = 1; i < n; i++)
            {
                while (!int.TryParse(Console.ReadLine(), out num))
                {
                    Console.WriteLine("Invalid input, please try adding again...");
                }
                tail.next = new ListNode(num);
                tail = tail.next;
            }

            int pos;
            Console.WriteLine("\nEnter the Position for looping: ");
            while (!int.TryParse(Console.ReadLine(), out pos))
            {
                Console.WriteLine("Invalid input, please try again...");
                Console.WriteLine("\nEnter the Position for looping: ");
            }

            loopHere(head, tail, pos);

            return head;
        }

        public void PrintLinkedList(ListNode head)
        {
            ListNode current = head;
            while (current != null)
            {
                Console.Write(current.val + " ");
                current = current.next;
            }
            Console.WriteLine();
        }


        void loopHere(ListNode head, ListNode tail, int position)
        {
            if (position == 0 || position == -1)
                return;

            ListNode walk = head;
            for (int i = 1; i < position; i++)
                walk = walk.next;
            tail.next = walk;
        }

        public async Task<bool> hasCycle(ListNode head)
        {
            if (head == null)
                return false;

            ListNode fastPtr = head;
            ListNode slowPtr = head;

            while (fastPtr != null && fastPtr.next != null)
            {
                fastPtr = fastPtr.next.next;
                slowPtr = slowPtr.next;
                if (fastPtr == slowPtr)
                {
                    return true;
                }
            }

            await Task.Delay(0);

            return false;
            
        }

    }
}
