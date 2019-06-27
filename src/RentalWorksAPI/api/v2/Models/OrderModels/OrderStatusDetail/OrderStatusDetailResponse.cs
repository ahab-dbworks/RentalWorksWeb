using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v2.Models.OrderModels.OrderStatusDetail
{
    public class OrderStatusDetailResponse
    {
        [DataType(DataType.Text)]
        public string orderid               { get; set; }

        [DataType(DataType.Text)]
        public string orderdesc             { get; set; }

        public List<OrderStatusItems> items { get; set; }
    }
}