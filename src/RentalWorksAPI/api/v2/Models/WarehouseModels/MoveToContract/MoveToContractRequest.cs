using System.Collections.Generic;

namespace RentalWorksAPI.api.v2.Models.WarehouseModels.MoveToContract
{
    public class MoveToContractRequest
    {
        public string orderid         { get; set; }
        public string webusersid      { get; set; }
        public List<MoveToContactItem> items { get; set; }
    }
}