using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Core.Controllers
{
    [Authorize]
    public class RegistrationController : Controller
    {
        // GET: /<controller>/
        [Route("patient-registration/new-patient")]
        public IActionResult NewPatient()
        {
            return View();
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
    }
}
