using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApplicationModelLibrary
{
    public class Customer : IEquatable<Customer>, IComparable<Customer>
    {
        public int Id { get; set; }
        public string Phone { get; set; } = String.Empty;
        public string Name { get; set; }
        public int Age { get; set; }
        [ExcludeFromCodeCoverage]
        public int CompareTo(Customer? other)
        {
            if (this.Age == other.Age)
                return 0;
            else if (this.Age < other.Age)
                return -1;
            else
                return 1;
            //return this.Age.CompareTo(other.Age);
        }
        [ExcludeFromCodeCoverage]
        public bool Equals(Customer? other)
        {
            return this.Id.Equals(other.Id);
        }
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return Id + " " + Name + " " + Age + " " + Phone;
        }
    }

    [ExcludeFromCodeCoverage]
    public class SortByCustomerName : IComparer<Customer>
    {

        public int Compare(Customer? x, Customer? y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
