using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v2.Models.WarehouseModels.UnstageItemModels
{
    public class UnstageItemResponse
    {
        [DataType(DataType.Text)]
        public string orderid                      { get; set; }

        public List<UnstageItemResponseItem> items { get; set; }
    }
}