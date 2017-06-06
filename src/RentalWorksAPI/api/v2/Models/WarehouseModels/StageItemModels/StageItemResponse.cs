using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.WarehouseModels.StageItemModels
{
    public class StageItemResponse
    {
        public StageItemQry order { get; set; } = new StageItemQry();
    }
}