using System;
using System.Collections.Generic;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.ViewModel
{
    public class RegistrationNewViewModel
    {
        public string DoB { get; set; }
        public string Currency { get; set; }
        public int Room { get; set; }

        public Patient Patient { get; set; }
        public NextOfKin NextOfKin { get; set; }
        public BillableServices Billables { get; set; }

        public List<SelectListItem> Gender { get; set; }
        public List<SelectListItem> Nation { get; set; }
        public List<SelectListItem> Relation { get; set; }
        public List<SelectListItem> Destination { get; set; }

        public RegistrationNewViewModel()
        {
            DoB = "";
            Room = 0;
            Currency = new GlobalService().GetGlobalProperties(_Constants.BASE_CURRENCY).Value;
            Patient = new Patient();
            NextOfKin = new NextOfKin();
            Billables = new BillableServices();

            Gender = InitializeGender();
            Nation = InitializeNationality();
            Relation = InitializeRelationship();
            Destination = InitializeRoomToSend();
        }

        private List<SelectListItem> InitializeGender()
        {
            List<SelectListItem> gender = new List<SelectListItem>();
            gender.Add(new SelectListItem("Male", "male"));
            gender.Add(new SelectListItem("Female", "female"));

            return gender;
        }

        private List<SelectListItem> InitializeNationality()
        {
            List<SelectListItem> nationality = new List<SelectListItem>();
            nationality.Add(new SelectListItem("Congo", "cg"));
            nationality.Add(new SelectListItem("Kenya", "ke"));
            nationality.Add(new SelectListItem("Uganda", "ug"));
            nationality.Add(new SelectListItem("Tanzania", "tz"));

            return nationality;
        }

        private List<SelectListItem> InitializeRelationship()
        {
            List<SelectListItem> relationship = new List<SelectListItem>();
            relationship.Add(new SelectListItem("Father", "father"));
            relationship.Add(new SelectListItem("Mother", "mother"));
            relationship.Add(new SelectListItem("Sibling", "sibling"));
            relationship.Add(new SelectListItem("Spouse", "spouse"));
            relationship.Add(new SelectListItem("Guardian", "guardian"));

            return relationship;
        }

        private List<SelectListItem> InitializeRoomToSend()
        {
            List<SelectListItem> relationship = new List<SelectListItem>();
            relationship.Add(new SelectListItem("Triage", "1"));
            relationship.Add(new SelectListItem("Casualty", "3"));
            relationship.Add(new SelectListItem("OPD Clinic", "2"));
            relationship.Add(new SelectListItem("Special Clinc", "4"));

            return relationship;
        }
    }
}
