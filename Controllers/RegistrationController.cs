using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Models;
using Core.ViewModel;
using Core.Services;
using System.Globalization;

namespace Core.Controllers
{
    [Authorize]
    public class RegistrationController : Controller
    {
        [BindProperty]
        public RegistrationNewViewModel Input { get; set; }

        [Route("patient-registration/new-patient")]
        public IActionResult NewPatient(RegistrationNewViewModel model, BillingService billing)
        {
            model.Billables = billing.GetBillableServices(_Constants.INITIAL_REG_FEE);
            return View(model);
        }

        [Route("patient-registration/edit-details")]
        public IActionResult EditPatient()
        {
            return View();
        }

        [Route("patient-registration/revisit")]
        public IActionResult RevisitPatient()
        {
            return View();
        }

        [Route("patient-registration/search")]
        public IActionResult SearchPatient()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PostRegistration(RegistrationNewViewModel model)
        {
            Input.Patient.Person.DoB = DateTime.ParseExact(Input.DoB, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Input.Patient.Save();

            Input.NextOfKin.Person = Input.Patient.Person;
            Input.NextOfKin.Save();

            Bills bill = new Bills
            {
                Amount = Input.Billables.Amount,
                Patient = Input.Patient,
                Notes = "New Patient Visit"
            };

            if (bill.Amount.Equals(0))
                bill.Flag = 1;
            
            bill.Save();

            BillsItems items = new BillsItems
            {
                Bill = bill,
                Item = Input.Billables,
                Amount = Input.Billables.Amount,
                Description = Input.Billables.Description
            };

            items.Save();

            PatientQueue queue = new PatientQueue
            {
                Patient = Input.Patient,
                Queue = new Queue(Input.Room),
                Bill = bill
            };

            queue.Save();

            return LocalRedirect("/patient-registration/new-patient");
        }

        [AllowAnonymous]
        public string GetBirthdateFromString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "";

            try
            {
                return DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("...");
            }

            if (IsNumber(value))
            {
                return DateTime.Now.AddYears(0 - int.Parse(value)).ToString("dd/MM/yyyy");
            }

            if (value.Contains('y'))
            {
                value = value.Replace("y", string.Empty);

                if (IsNumber(value))
                    return DateTime.Now.AddYears(0 - int.Parse(value)).ToString("dd/MM/yyyy");
                else
                    return "";
            }
            else if (value.Contains('m'))
            {
                value = value.Replace("m", string.Empty);

                if (IsNumber(value))
                    return DateTime.Now.AddMonths(0 - int.Parse(value)).ToString("dd/MM/yyyy");
                else
                    return "";
            }
            else if (value.Contains('w'))
            {
                value = value.Replace("w", string.Empty);

                if (IsNumber(value))
                    return DateTime.Now.AddDays(0 - (int.Parse(value) * 7)).ToString("dd/MM/yyyy");
                else
                    return "";
            }
            else if (value.Contains('d'))
            {
                value = value.Replace("d", string.Empty);

                if (IsNumber(value))
                    return DateTime.Now.AddDays(0 - int.Parse(value)).ToString("dd/MM/yyyy");
                else
                    return "";
            }

            return "";
        }

        [AllowAnonymous]
        public Boolean IsNumber(String s)
        {
            Boolean value = true;
            foreach (Char c in s.ToCharArray())
            {
                value = value && Char.IsDigit(c);
            }

            return value;
        }
    }
}
