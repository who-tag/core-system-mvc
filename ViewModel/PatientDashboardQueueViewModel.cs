using System;
using System.Collections.Generic;
using Core.Models;

namespace Core.ViewModel
{
    public class PatientDashboardQueueViewModel
    {
        public Queue queue { get; set; }
        public List<PatientQueue> pq { get; set; }

        public PatientDashboardQueueViewModel()
        {
            queue = new Queue();
            pq = new List<PatientQueue>();
        }
    }
}
