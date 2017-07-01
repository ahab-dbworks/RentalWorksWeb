using System.Collections.Generic;

namespace RentalWorksAPI.api.v2.Models.WarehouseModels.MoveToContract
{
    public class MoveToContractResponse
    {
        public string orderid                         { get; set; }
        public List<MoveToContractResponseItem> items { get; set; }
    }
}