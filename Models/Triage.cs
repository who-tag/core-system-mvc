using System;
namespace Core.Models
{
    public class Triage
    {
        public int Id { get; set; }
        public int Flag { get; set; }
        public int Void { get; set; }

        public Encounter Encounter { get; set; }

        public DateTime Start { get; set; }
        public DateTime? Ended { get; set; }
        public DateTime? LMP { get; set; }

        public Double? Temparature { get; set; }
        public Double? Systolic { get; set; }
        public Double? Diastolic { get; set; }

        public Double? RespiratoryRate { get; set; }
        public Double? PulseRate { get; set; }
        public Double? OxystenSaturation { get; set; }

        public Double? Weight { get; set; }
        public Double? Height { get; set; }
        public Double? BMI { get; set; }

        public Double? MUAC { get; set; }
        public Double? Chest { get; set; }
        public Double? Abdominal { get; set; }

        public Triage()
        {
            Id = 0;
            Flag = 0;
            Void = 0;

            Start = DateTime.Now;
            Ended = null;
            LMP = null;

            Weight = 0;
            Height = 0;
            BMI = 0;
            Weight = 0;
        }

        public Triage(int idnt) : this()
        {
            Id = idnt;
        }
    }
}
