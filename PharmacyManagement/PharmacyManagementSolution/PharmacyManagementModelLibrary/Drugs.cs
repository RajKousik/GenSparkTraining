using System.Data;

namespace PharmacyManagementModelLibrary
{
    public class Drugs
    {
        public int Id { get; set; }
        public string Name{ get; set; } = string.Empty;

        public string Brand{ get; set; } = string.Empty ;

        public string DosageForm { get; set; } = string.Empty;

        public string Strength { get; set; } = string.Empty;

        public int InStock{ get; set; }

        public DateTime ExpiryDate{ get; set; }

        public Double Price{ get; set; }

        public Drugs()
        {
            Name = "";
            Brand = "";
            DosageForm = "";
            Strength = "";
            InStock = 0;
            ExpiryDate = DateTime.MaxValue;
            Price = 0;
        }

        public Drugs(string name, string brand, string dosageForm, string strength, int inStock, DateTime expiryDate, double price)
        {
            Name = name;
            Brand = brand;
            DosageForm = dosageForm;
            Strength = strength;
            InStock = inStock;
            ExpiryDate = expiryDate;
            Price = price;
        }

        public override string ToString()
        {
            return "\nDrug Id : " + Id
                + "\nDrug Name : " + Name
                + "\nBrand : " + Brand
                + "\nDosage Form : " + DosageForm
                + "\nStrength : " + Strength
                + "\nInStock : " + InStock
                + "\nExpiry Date : " + ExpiryDate
                + "\nPrice : " + Price + "\n";
        }
        public override bool Equals(object? obj)
        {
            Drugs d1, d2;
            d1 = this;
            d2 = obj as Drugs;
            return d1.Id.Equals(d2.Id);
        }


    }
}
