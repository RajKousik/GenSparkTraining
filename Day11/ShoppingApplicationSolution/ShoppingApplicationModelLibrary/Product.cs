namespace ShoppingApplicationModelLibrary
{
    public class Product : IEquatable<Product>
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Image { get; set; }
        public int QuantityInHand { get; set; }
        public override string ToString()
        {
            return "Id : " + Id +
                "\nName : " + Name +
                "\nPrice : $" + Price +
                "\nNos in Stock : " + QuantityInHand;
        }

        public bool Equals(Product? other)
        {
            return this.Id.Equals(other.Id);
        }

        public Product()
        {

        }
        public Product(int id, double price, string name, int quantityInHand)
        {
            Id = id;
            Price = price;
            Name = name;
            QuantityInHand = quantityInHand;
        }
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
