using System.Collections.Generic;

namespace RentalWorksAPI.api.v2.Models.WarehouseModels.StageItemModels
{
    public class StageItemRequest
    {
        public string orderid        { get; set; }
        public string webusersid     { get; set; }
        public List<StageItem> items { get; set; }
    }
}