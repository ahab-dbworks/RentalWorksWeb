using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.WarehouseModels.StageItemModels
{
    public class StageItemRequest
    {
        public string orderid        { get; set; }
        public List<StageItem> items { get; set; }
    }
}