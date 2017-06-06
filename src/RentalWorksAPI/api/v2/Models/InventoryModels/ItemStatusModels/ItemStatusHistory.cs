using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.InventoryModels.ItemStatusModels
{
    public class ItemStatusHistory
    {
        public string type { get; set; } = string.Empty;
        public string datetime { get; set; } = string.Empty;
        public string orderid { get; set; } = string.Empty;
        public string orderno { get; set; } = string.Empty;
        public string dealname { get; set; } = string.Empty;
        public string qty { get; set; } = string.Empty;
        public List<ItemStatusHistory> transactions { get; set; } = new List<ItemStatusHistory>();
    }
}