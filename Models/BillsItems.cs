using System;
using Core.Services;

namespace Core.Models
{
    public class BillsItems
    {
        public int Id { get; set; }
        public Bills Bill { get; set; }
        public BillableServices Item { get; set; }
        public Double Cost { get; set; }
        public Double Amount { get; set; }
        public String Description { get; set; }

        public BillsItems()
        {
            Id = 0;
            Cost = 0;
            Amount = 0;
            Description = "";

            Bill = new Bills();
            Item = new BillableServices();
        }

        public BillsItems Save(){
            return new BillingService().SaveBillsItems(this);
        }
    }
}
