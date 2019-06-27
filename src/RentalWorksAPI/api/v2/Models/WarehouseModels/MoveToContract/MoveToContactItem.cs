using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v2.Models.WarehouseModels.MoveToContract
{
    public class MoveToContactItem
    {
        [DataType(DataType.Text)]
        public string barcode      { get; set; } = "";

        [DataType(DataType.Text)]
        public string masteritemid { get; set; } = "";

        public int qty             { get; set; } = 0;
    }
}