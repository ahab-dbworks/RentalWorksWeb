using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.WarehouseModels.StageItemModels
{
    public class StageItem
    {
        [DataType(DataType.Text)]
        public string barcode      { get; set; } = "";

        [DataType(DataType.Text)]
        public string masteritemid { get; set; } = "";

        public int qty             { get; set; } = 0;
    }
}