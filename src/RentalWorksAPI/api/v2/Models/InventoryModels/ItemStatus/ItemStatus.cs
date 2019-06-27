using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v2.Models.InventoryModels.ItemStatus
{
    public class ItemStatus
    {
        [DataType(DataType.Text)]
        public string barcode  { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string serialno { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string rfid     { get; set; } = string.Empty;

        public int? days       { get; set; } = 0;
    }
}