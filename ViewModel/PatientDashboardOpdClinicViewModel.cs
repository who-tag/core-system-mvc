using System;
using Core.Models;

namespace Core.ViewModel
{
    public class PatientDashboardOpdClinicViewModel
    {
        public Queue queue { get; set; }
        public PatientQueue pq { get; set; }
        public ClinicalNotes notes { get; set; }

        public PatientDashboardOpdClinicViewModel()
        {
            queue = new Queue();
            pq = new PatientQueue();

            notes = new ClinicalNotes();
        }
    }
}
