using System;
using Core.Services;

namespace Core.Models
{
    public class PatientQueue
    {
        public int Id { get; set; }
        public int Seen { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public Queue Queue { get; set; }
        public Patient Patient { get; set; }
        public Bills Bill { get; set; }
        public Users Provider { get; set; }
        public int Priority { get; set; }
        public string Notes { get; set; }

        public PatientQueue()
        {
            Id = 0;
            Seen = 0;
            Priority = 0;
            Notes = "";

            Date = DateTime.Now.Date;
            Time = DateTime.Now.TimeOfDay;

            Queue = new Queue();
            Patient = new Patient();
            Bill = new Bills();
            Provider = new Users();
        }

        public PatientQueue Save(){
            return new PatientService().SavePatientQueue(this);
        }

        public PatientQueue UpdateProvider(){
            return new PatientService().UpdatePatientQueueProvider(this);
        }

    }
}
