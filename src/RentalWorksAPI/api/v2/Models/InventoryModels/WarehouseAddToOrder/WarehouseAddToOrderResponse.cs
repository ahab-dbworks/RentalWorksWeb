using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.InventoryModels.WarehouseAddToOrder
{
    public class WarehouseAddToOrderResponse
    {
        public List<WarehouseAddToOrderItem> items { get; set; } = new List<WarehouseAddToOrderItem>();
    }
}