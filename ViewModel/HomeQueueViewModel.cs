using System;
using System.Collections.Generic;
using Core.Models;

namespace Core.ViewModel
{
    public class HomeQueueViewModel
    {
        public Queue queue { get; set; }
        public List<PatientQueue> pq { get; set; }

        public HomeQueueViewModel()
        {
            queue = new Queue();
            pq = new List<PatientQueue>();
        }
    }
}
