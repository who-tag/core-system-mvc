using System;
namespace Core.Models
{
    public class Encounter
    {
        public int Id { get; set; }
        public int Void { get; set; }
        public DateTime Date { get; set; }

        public Encounter()
        {
            Id = 0;
            Void = 0;
            Date = DateTime.Now;
        }
    }
}
