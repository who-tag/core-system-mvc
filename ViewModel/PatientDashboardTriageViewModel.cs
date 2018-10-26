using System;
using Core.Models;

namespace Core.ViewModel
{
    public class PatientDashboardTriageViewModel
    {
        public Queue queue { get; set; }
        public PatientQueue pq { get; set; }

        public PatientDashboardTriageViewModel()
        {
            queue = new Queue();
            pq = new PatientQueue();
        }
    }
}
