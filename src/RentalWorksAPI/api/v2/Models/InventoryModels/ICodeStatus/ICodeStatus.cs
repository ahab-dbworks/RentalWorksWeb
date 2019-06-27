using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v2.Models.InventoryModels.ICodeStatus
{
    public class ICodeStatus
    {
        [DataType(DataType.Text)]
        public string warehouseid           { get; set; }

        [DataType(DataType.Text)]
        public string masterid              { get; set; } = string.Empty;

        public int?   transactionhistoryqty { get; set; } = 0;
    }
}