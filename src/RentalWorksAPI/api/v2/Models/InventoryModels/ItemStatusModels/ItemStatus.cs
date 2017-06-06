using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.InventoryModels.ItemStatusModels
{
    public class ItemStatus
    {
        public string rentalitemid { get; set; } = string.Empty;
        public string masterid { get; set; } = string.Empty;
        public string masterno { get; set; } = string.Empty;
        public string master { get; set; } = string.Empty;
        public string mfgserial { get; set; } = string.Empty;
        public string rfid { get; set; } = string.Empty;
        public string barcode { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public List<ItemStatusHistory> transactions { get; set; } = new List<ItemStatusHistory>();
    }
}