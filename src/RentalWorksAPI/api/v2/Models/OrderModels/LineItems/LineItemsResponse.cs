using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.OrderModels.LineItems
{
    public class LineItemsResponse
    {
        public LineItemOrder order { get; set; } = new LineItemOrder();
    }
}