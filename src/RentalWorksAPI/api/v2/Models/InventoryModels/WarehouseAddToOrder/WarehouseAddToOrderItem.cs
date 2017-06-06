using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.InventoryModels.WarehouseAddToOrder
{
    public class WarehouseAddToOrderItem
    {
        public string masterid { get; set; } = string.Empty;
        public string masterno { get; set; } = string.Empty;
        public string master { get; set; } = string.Empty;
        public string departmentid { get; set; } = string.Empty;
        public string department { get; set; } = string.Empty;
    }
}