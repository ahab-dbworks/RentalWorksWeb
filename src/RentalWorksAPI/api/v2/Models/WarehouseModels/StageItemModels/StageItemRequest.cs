using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.WarehouseModels.StageItemModels
{
    public class StageItemRequest
    {
        public string orderid { get; set; } = string.Empty;
        public List<StageItem> items { get; set; } = new List<StageItem>();
    }
}