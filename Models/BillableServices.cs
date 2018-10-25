using System;
namespace Core.Models
{
    public class BillableServices
    {
        public int Id { get; set; }
        public String Code { get; set; }
        public String Name { get; set; }
        public Double Amount { get; set; }
        public String Description { get; set; }

        public BillableServices()
        {
            Id = 0;
            Code = "";
            Name = "";
            Amount = 0;
            Description = "";
        }
    }
}
