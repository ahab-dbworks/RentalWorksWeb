using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.OrderModels.OrderStatusDetailModels
{
    public class OrderStatusDetailResponseOrder
    {
        public string orderid { get; set; } = string.Empty;
        public string orderdesc { get; set; } = string.Empty;
        public List<OrderStatus> items { get; set; } = new List<OrderStatus>();
    }
}