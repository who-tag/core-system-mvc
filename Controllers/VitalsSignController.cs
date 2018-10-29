using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    public class VitalsSignController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }

        [Route("triage/vital-signs")]
        public IActionResult Vitals()
        {
            return View();
        }
    }
}
