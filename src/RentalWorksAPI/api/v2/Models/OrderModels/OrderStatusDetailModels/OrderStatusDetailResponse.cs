using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.OrderModels.OrderStatusDetailModels
{
    public class OrderStatusDetailResponse
    {
        public OrderStatusDetailResponseOrder order { get; set; } = new OrderStatusDetailResponseOrder();
    }
}