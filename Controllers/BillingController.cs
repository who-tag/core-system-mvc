using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using Core.Models;
using Core.ViewModel;
using Core.Services;
using Microsoft.AspNetCore.Authorization;

namespace Core.Controllers
{
    [Authorize]
    public class BillingController : Controller
    {
        [Route("accounts/billing/cashier")]
        public IActionResult Cashier(BillingCashierViewModel model, BillingService service)
        {
            model.bills = service.GetBills(new List<int>(new int[] { 0 }), null);
            return View(model);
        }

        [AllowAnonymous]
        public JsonResult GetBillItems(int idnt, BillingService service)
        {
            List<BillsItems> items = service.GetBillItems(new Bills(idnt));
            return Json(items);
        }

        [AllowAnonymous]
        public string FlagBill(int idnt, int flag, BillingService service)
        {
            Bills bill = new Bills(idnt);
            bill.Flag = flag;
            bill.UpdateFlag();

            return "success";
        }
    }
}
