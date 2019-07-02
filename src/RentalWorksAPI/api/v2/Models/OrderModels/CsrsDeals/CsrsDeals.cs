using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.OrderModels.CsrsDeals
{
    public class CsrsDeals
    {
        [DataType(DataType.Text)]
        public string locationid  { get; set; }

        [DataType(DataType.Text)]
        public List<string> csrid { get; set; }
    }
}