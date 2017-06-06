using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.OrderModels.OrderStatusDetailModels
{
    public class OrderStatus
    {
        public string rectype { get; set; } = string.Empty;
        public string masterid { get; set; } = string.Empty;
        public string masterno { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public int qtyordered { get; set; } = 0;
        public int qtystaged { get; set; } = 0;
        public int qtyout { get; set; } = 0;
        public int qtyin { get; set; } = 0;
        public int qtystillout { get; set; } = 0;
        public string itemorder { get; set; } = string.Empty;
        public string trackedby { get; set; } = string.Empty;
        public List<OrderStatusDetail> physicalassets { get; set; } = new List<OrderStatusDetail>();
    }
}