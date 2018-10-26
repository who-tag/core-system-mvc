using System;
using System.Collections.Generic;
using Core.Models;

namespace Core.ViewModel
{
    public class BillingCashierViewModel
    {
        public List<Bills> bills { get; set; }

        public BillingCashierViewModel()
        {
            bills = new List<Bills>();
        }
    }
}
