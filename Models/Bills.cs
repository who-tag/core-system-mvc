using System;
using Core.Services;

namespace Core.Models
{
    public class Bills
    {
        public int Id { get; set; }
        public int Flag { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public Patient Patient { get; set; }
        public Double Cost { get; set; }
        public Double Amount { get; set; }
        public String Notes { get; set; }

        public Bills()
        {
            Id = 0;
            Flag = 0;
            Date = DateTime.Now.Date;
            Time = DateTime.Now.TimeOfDay;

            Patient = new Patient();

            Cost = 0;
            Amount = 0;
            Notes = "";
        }

        public Bills(int idnt)
        {
            Id = idnt;
            Patient = new Patient();

            Cost = 0;
            Amount = 0;
            Notes = "";
        }

        public Bills Save(){
            return new BillingService().SaveBills(this);
        }

        public Bills UpdateFlag(){
            return new BillingService().UpdateBillsFlag(this);
        }
    }
}
