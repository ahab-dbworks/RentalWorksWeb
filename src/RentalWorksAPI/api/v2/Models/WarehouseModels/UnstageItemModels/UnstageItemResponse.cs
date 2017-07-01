using System.Collections.Generic;

namespace RentalWorksAPI.api.v2.Models.WarehouseModels.UnstageItemModels
{
    public class UnstageItemResponse
    {
        public string orderid                      { get; set; }
        public List<UnstageItemResponseItem> items { get; set; }
    }
}