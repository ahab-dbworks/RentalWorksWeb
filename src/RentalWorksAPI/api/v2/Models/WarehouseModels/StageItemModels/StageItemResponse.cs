using System.Collections.Generic;

namespace RentalWorksAPI.api.v2.Models.WarehouseModels.StageItemModels
{
    public class StageItemResponse
    {
        public string orderid            { get; set; }
        public List<StageItemResponseItem> items { get; set; }
    }
}