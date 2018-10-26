using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using Core.ViewModel;
using Core.Services;

namespace Core.Controllers
{
    public class BillingController : Controller
    {
        [Route("accounts/billing/cashier")]
        public IActionResult Cashier(BillingCashierViewModel model, BillingService service)
        {
            model.bills = service.GetBills(new List<int>(new int[] { 0 }), null);
            return View(model);
        }
    }
}
