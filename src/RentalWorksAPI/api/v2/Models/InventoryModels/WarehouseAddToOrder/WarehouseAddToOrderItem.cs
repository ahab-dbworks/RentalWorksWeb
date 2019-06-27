using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.InventoryModels.WarehouseAddToOrder
{
    public class WarehouseAddToOrderItem
    {
        [DataType(DataType.Text)]
        public string masterid     { get; set; }

        [DataType(DataType.Text)]
        public string masterno     { get; set; }

        [DataType(DataType.Text)]
        public string master       { get; set; }

        [DataType(DataType.Text)]
        public string departmentid { get; set; }

        [DataType(DataType.Text)]
        public string department   { get; set; }

        [DataType(DataType.Text)]
        public string warehouseid  { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class WarehousesAddToOrder
    {
        public List<string> warehouseids { get; set; } = new List<string>();
    }
    //----------------------------------------------------------------------------------------------------
}