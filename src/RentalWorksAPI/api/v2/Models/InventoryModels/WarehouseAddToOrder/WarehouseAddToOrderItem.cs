using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.InventoryModels.WarehouseAddToOrder
{
    public class WarehouseAddToOrderItem
    {
        public string masterid     { get; set; }
        public string masterno     { get; set; }
        public string master       { get; set; }
        public string departmentid { get; set; }
        public string department   { get; set; }
        public string warehouseid  { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class WarehousesAddToOrder
    {
        public List<string> warehouseids { get; set; } = new List<string>();
    }
    //----------------------------------------------------------------------------------------------------
}