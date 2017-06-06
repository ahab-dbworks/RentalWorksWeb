using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.OrderModels.LineItems
{
    public class LineItemOrder
    {
        public string orderid { get; set; } = string.Empty;
        public List<LineItem> items { get; set; } = new List<LineItem>();
    }
}