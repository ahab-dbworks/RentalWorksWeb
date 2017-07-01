using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.WarehouseModels.UnstageItemModels
{
    public class UnstageItemRequest
    {
        public string orderid                     { get; set; }
        public string webusersid                  { get; set; }
        public List<UnstageItem> items { get; set; }
    }
}