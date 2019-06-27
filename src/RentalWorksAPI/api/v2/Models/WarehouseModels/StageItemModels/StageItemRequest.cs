using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v2.Models.WarehouseModels.StageItemModels
{
    public class StageItemRequest
    {
        [DataType(DataType.Text)]
        public string orderid        { get; set; }

        [DataType(DataType.Text)]
        public string webusersid     { get; set; }

        public List<StageItem> items { get; set; }
    }
}