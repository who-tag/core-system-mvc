using System.Collections.Generic;
using System.Diagnostics;
using Core.ViewModel;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Core.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("programs/all-programs")]
        public IActionResult Programs()
        {
            return View();
        }

        [Route("queue/{code}")]
        public IActionResult Queue(string code, HomeQueueViewModel model, PatientService svc)
        {
            model.queue = svc.GetQueue(code);
            model.pq = svc.GetPatientQueues(model.queue);
            
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
