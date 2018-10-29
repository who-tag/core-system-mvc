using System;
namespace Core.Models
{
    public class Assets
    {
        public int Id { get; set; }
        public int Void { get; set; }
        public String Code { get; set; }
        public String Description { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Double PurchaseAmount { get; set; }
        public String PurchaseCurrency { get; set; }
        public DateTime ManufactureDate { get; set; }
        public String ManufactureCompany { get; set; }
        public String Supplier { get; set; }
        public String DeliveryNumber { get; set; }
        public String AllocatedTo { get; set; }
        public String Notes { get; set; }

        public AssetsCategory Category { get; set; }
        public AssetsStatus Status { get; set; }

        public Assets()
        {
            Id = 0;
            Void = 0;

            Code = "";
            Description = "";
            PurchaseAmount = 0;
            PurchaseCurrency = "";
            ManufactureCompany = "";
            Supplier = "";
            DeliveryNumber = "";
            AllocatedTo = "";
            Notes = "";

            Status = new AssetsStatus();
            Category = new AssetsCategory();
        }

        public Assets(int idnt) : this()
        {
            Id = idnt;
        }

        public Assets(int idnt, String code) : this(idnt)
        {
            Code = code;
        }
    }
}
