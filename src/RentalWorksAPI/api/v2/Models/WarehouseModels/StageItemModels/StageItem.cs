﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.WarehouseModels.StageItemModels
{
    public class StageItem
    {
        public string barcode      { get; set; }
        public string masteritemid { get; set; }
        public int qty             { get; set; }
    }
}