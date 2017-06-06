using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.WarehouseModels.StageItemModels
{
    public class StageItemItem
    {
        public string statuscode { get; set; } = string.Empty;
        public string statusmessage { get; set; } = string.Empty;
        public string barcode { get; set; } = string.Empty;
        public string masteritemid { get; set; } = string.Empty;
        public string masterid { get; set; } = string.Empty;
        public decimal quantity { get; set; } = 0;
        public string datetimestamp { get; set; } = string.Empty;
    }
}