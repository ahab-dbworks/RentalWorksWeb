using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.WarehouseModels.MoveToContract
{
    public class Order
    {
        public string orderid { get; set; } = string.Empty;
        public List<MoveToContractSp> items { get; set; } = new List<MoveToContractSp>();
    }
}