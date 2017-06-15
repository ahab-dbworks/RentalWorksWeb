using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.OrderModels.CsrsDeals
{
    public class CsrsDeals
    {
        public string locationid  { get; set; }
        public List<string> csrid { get; set; }
    }
}