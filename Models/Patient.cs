using System;
using Core.Services;

namespace Core.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public Person Person { get; set; }

        public Patient()
        {
            Id = 0;
            Person = new Person();
        }

        public Patient Save(){
            Person.Save();
            return new PatientService().SavePatient(this);
        }
    }
}
