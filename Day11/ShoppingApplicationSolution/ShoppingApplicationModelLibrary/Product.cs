using System.Diagnostics.CodeAnalysis;

namespace ShoppingApplicationModelLibrary
{
    public class Product : IEquatable<Product>
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Name { get; set; } = string.Empty;
        [ExcludeFromCodeCoverage]
        public string? Image { get; set; }
        public int QuantityInHand { get; set; }

        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return "Id : " + Id +
                "\nName : " + Name +
                "\nPrice : Rs." + Price +
                "\nNos in Stock : " + QuantityInHand;
        }
        [ExcludeFromCodeCoverage]
        public bool Equals(Product? other)
        {
            return this.Id.Equals(other.Id);
        }

        public Product()
        {

        }

        [ExcludeFromCodeCoverage]
        public Product(int id, double price, string name, int quantityInHand)
        {
            Id = id;
            Price = price;
            Name = name;
            QuantityInHand = quantityInHand;
        }

        [ExcludeFromCodeCoverage]
        public Product(int id, double price, string name, string? image, int quantityInHand)
        {
            Id = id;
            Price = price;
            Name = name;
            Image = image;
            QuantityInHand = quantityInHand;
        }

    }
}
