using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v2.Models.WarehouseModels.StageItemModels
{
    public class StageItemResponse
    {
        [DataType(DataType.Text)]
        public string orderid            { get; set; }

        public List<StageItemResponseItem> items { get; set; }
    }
}