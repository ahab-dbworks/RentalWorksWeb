using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.WarehouseModels.UnstageItemModels
{
    public class UnstageItemRequest
    {
        public string orderid { get; set; }
        public List<UnstageItemRequestItem> items { get; set; }
    }
}