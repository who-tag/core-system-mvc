using System;
using Core.Services;
namespace Core.Models
{
    public class NextOfKin
    {
        public int Id { get; set; }
        public Person Person { get; set; }
        public string Name { get; set; }
        public string Relation { get; set; }
        public string Physical { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        public NextOfKin()
        {
            Id = 0;
            Person = new Person();
            Name = "";
            Relation = "";
            Physical = "";
            Mobile = "";
            Email = "";
        }

        public NextOfKin Save()
        {
            return new PatientService().SaveNextOfKin(this);
        }
    }
}
