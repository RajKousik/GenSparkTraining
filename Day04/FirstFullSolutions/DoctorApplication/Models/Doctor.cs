using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorApplication.Models
{
    internal class Doctor
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Experience { get; set; }
        public string Qualification { get; set; }

        public string Speciality { get; set; }

        /// <summary>
        /// Default Constrcutor for initializing data members
        /// </summary>
        public Doctor()
        {
            Id = 0;
            Name = string.Empty;
            Age = 0;
            Experience = 0;
            Qualification = string.Empty;
            Speciality = string.Empty;
        }

        /// <summary>
        /// Parameterized Constructor for initializing data members
        /// </summary>
        /// <param name="id">Doctor's ID</param>
        /// <param name="name">Doctor's name as string</param>
        /// <param name="age">Doctor's age</param>
        /// <param name="experience">Doctor's experience in years</param>
        /// <param name="qualification">Doctor's qualification</param>
        /// <param name="speciality">Doctor's speciality as string</param>
        public Doctor(int id, string name, int age, int experience, string qualification, string speciality) : this(id)
        {
            Name = name;
            Age = age;
            Experience = experience;
            Qualification= qualification;
            Speciality = speciality;
        }

        /// <summary>
        /// Parameterized constructor for initializing id
        /// </summary>
        /// <param name="id"></param>
        public Doctor(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Prints all the Doctor details
        /// </summary>
        public void PrintDoctorDetails()
        {
            Console.WriteLine($"ID:\t{Id}");
            Console.WriteLine($"Name:\t{Name}");
            Console.WriteLine($"Age:\t{Age}");
            Console.WriteLine($"Experience:\t{Experience}");
            Console.WriteLine($"Qualification:\t{Qualification}");
            Console.WriteLine($"Speciality:\t{Speciality}");
            Console.WriteLine("\n");
        }

    }
}
