using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.WarehouseModels.MoveToContract
{
    public class MoveToContractRequest
    {
        public string usersid { get; set; } = string.Empty;
        public string orderid { get; set; } = string.Empty;
        public List<StagedItem> items { get; set; } = new List<StagedItem>();
    }
}