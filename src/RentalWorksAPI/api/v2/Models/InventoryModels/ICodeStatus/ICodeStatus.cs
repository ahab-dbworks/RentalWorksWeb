namespace RentalWorksAPI.api.v2.Models.InventoryModels.ICodeStatus
{
    public class ICodeStatus
    {
        public string warehouseid           { get; set; }
        public string masterid              { get; set; } = string.Empty;
        public int?   transactionhistoryqty { get; set; } = 0;
    }
}