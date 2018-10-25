using System;
using Core.Services;

namespace Core.Models
{
    public class Person
    {
        public int Id { get; set; }
        public DateTime DoB { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Gender { get; set; }
        public string Citizen { get; set; }
        public string Physical { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Residence { get; set; }

        public Person()
        {
            Id = 0;
            Name = "";
            NickName = "";
            Gender = "";
            Citizen = "";
            Physical = "";
            Mobile = "";
            Email = "";
            Residence = "";
        }

        public string GetAgeInyears(){
            return "22 yrs";
        }

        public Person Save(){
            return new PatientService().SavePerson(this);
        }
    }
}
