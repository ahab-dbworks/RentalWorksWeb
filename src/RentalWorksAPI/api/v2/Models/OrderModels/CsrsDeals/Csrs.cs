using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.OrderModels.CsrsDeals
{
    public class Csrs
    {
        public string     csrid { get; set; }
        public List<Deal> deals { get; set; }
    }
}