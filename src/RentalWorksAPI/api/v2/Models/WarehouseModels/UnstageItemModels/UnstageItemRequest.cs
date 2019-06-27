using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.WarehouseModels.UnstageItemModels
{
    public class UnstageItemRequest
    {
        [DataType(DataType.Text)]
        public string orderid                     { get; set; }

        [DataType(DataType.Text)]
        public string webusersid                  { get; set; }

        public List<UnstageItem> items { get; set; }
    }
}