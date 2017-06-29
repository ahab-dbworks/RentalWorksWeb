using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.InventoryModels.ItemStatus
{
    public class ItemStatusHistory
    {
        public string type     { get; set; }
        public string datetime { get; set; }
        public string orderid  { get; set; }
        public string orderno  { get; set; }
        public string dealname { get; set; }
        public int qty         { get; set; }
    }
}