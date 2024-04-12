using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderstandingBasicsApp.Models
{
    class Employee
    {
        //int id;

        //public int GetId()
        //{ return id; }

        //public void SetId(int id)
        //{
        //    this.id = id;
        //}

        //public int Id
        //{
        //    get { return id; }
        //    set { id = value; }
        //}

        //public int Age
        //{
        //    get => Age;
        //    set => id = value;
        //}

        public int Id {get; private set;}
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email{ get; set; }
        public double Salary{ get; set; }


        public Employee()
        {
            Id = 0;
            Name = string.Empty;
            DateOfBirth = DateTime.MinValue;
            Email = string.Empty;
            Salary = 0;
        }

        public Employee(int id)
        {
            Id = id;
        }

        public Employee(string name)
        {
            Name = name;
        }

        public Employee(int id, string name, DateTime dateOfBirth, string email, double salary) : this(id)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            Email = email;
            Salary = salary;
        }

        public Employee(string name, DateTime dateOfBirth, string email, double salary)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            Email = email;
            Salary = salary;
        }

        /// <summary>
        /// starts a work and returns no of hours to be worked
        /// </summary>
        /// <param name="hours">hours of work</param>
        /// <returns>a string containing hours</returns>
        public string StartWork(int hours)
        {
            Console.WriteLine("Started");
            return $"You have to work for {hours}";
        }

        public void PrintEmployeeDetails()
        {
            Console.WriteLine($"{Id} {Name} {Salary} {Email} {DateOfBirth}");
        }


    }
}
