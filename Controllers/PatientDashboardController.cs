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
        [BindProperty]
        public PatientDashboardTriageViewModel Input { get; set; }

        [Route("queue/{code}")]
        public IActionResult Queue(string code, PatientDashboardQueueViewModel model, PatientService svc)
        {
            model.queue = svc.GetQueue(code);
            model.pq = svc.GetPatientQueues(model.queue);

            return View(model);
        }

        [Route("triage/{code}/{idnt}")]
        public IActionResult Triage(string code, int idnt, PatientDashboardTriageViewModel model, PatientService service)
        {
            model.queue = service.GetQueue(code);
            model.pq = service.GetPatientQueue(idnt);
            model.pq.Provider = new Users(1);
            model.pq.UpdateProvider();

            model.triage = new Triage();

            return View(model);
        }

        [Route("clinic/{code}/{idnt}")]
        public IActionResult OpdClinic(string code, int idnt, PatientDashboardOpdClinicViewModel model, PatientService service)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult PostTriage(PatientDashboardTriageViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(Input.lmp)){
                Input.triage.LMP = DateTime.Parse(Input.lmp);
            }

            return LocalRedirect("/patient-registration/new-patient");
        }
    }
}
