using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.WarehouseModels.MoveToContract
{
    public class StagedItem
    {
        public string barcode { get; set; } = string.Empty;
        public string masteritemid { get; set; } = string.Empty;
        public int quantity { get; set; } = 0;
    }
}