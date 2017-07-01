namespace RentalWorksAPI.api.v2.Models.WarehouseModels.UnstageItemModels
{
    public class UnstageItem
    {
        public string barcode      { get; set; } = "";
        public string masteritemid { get; set; } = "";
        public int qty             { get; set; } = 0;
    }
}