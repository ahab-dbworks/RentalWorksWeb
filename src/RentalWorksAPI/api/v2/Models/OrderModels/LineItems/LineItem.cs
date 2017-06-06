using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.OrderModels.LineItems
{
    public class LineItem
    {
        public string masteritemid { get; set; } = string.Empty;
        public string masterid { get; set; } = string.Empty;
        public string qtyordered { get; set; } = string.Empty;
        public string notes { get; set; } = string.Empty;
        public string parentid { get; set; } = string.Empty;
    }
}