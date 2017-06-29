namespace RentalWorksAPI.api.v2.Models.InventoryModels.ItemStatus
{
    public class ItemStatus
    {
        public string barcode  { get; set; } = string.Empty;
        public string serialno { get; set; } = string.Empty;
        public string rfid     { get; set; } = string.Empty;
        public int? days       { get; set; } = 0;
    }
}