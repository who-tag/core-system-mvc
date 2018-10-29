using System;
using System.Collections.Generic;
using Core.Models;

namespace Core.ViewModel
{
    public class BillingAssetsViewModel
    {
        public List<Assets> Assets { get; set; }

        public BillingAssetsViewModel()
        {
            Assets = new List<Assets>();
        }
    }
}
