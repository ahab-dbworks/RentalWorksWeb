using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v2.Models.WarehouseModels.MoveToContract
{
    public class MoveToContractResponse
    {
        [DataType(DataType.Text)]
        public string orderid                         { get; set; }

        public List<MoveToContractResponseItem> items { get; set; }
    }
}