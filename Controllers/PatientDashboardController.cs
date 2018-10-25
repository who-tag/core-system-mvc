using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Core.Models;
using Core.ViewModel;
using Core.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    [Authorize]
    public class PatientDashboardController : Controller
    {
        // GET: /<controller>/
        [Route("triage/{code}/{idnt}")]
        public IActionResult Triage(string code, int idnt, PatientDashboardTriageViewModel model, PatientService service)
        {
            model.queue = service.GetQueue(code);
            model.pq = service.GetPatientQueue(idnt);
            model.pq.Provider = new Users(1);
            model.pq.UpdateProvider();

            return View(model);
        }
    }
}
