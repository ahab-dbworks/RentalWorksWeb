using System.Collections.Generic;

namespace RentalWorksAPI.api.v2.Models.OrderModels.OrderStatusDetail
{
    public class OrderStatusDetailResponse
    {
        public string orderid               { get; set; }
        public string orderdesc             { get; set; }
        public List<OrderStatusItems> items { get; set; }
    }
}