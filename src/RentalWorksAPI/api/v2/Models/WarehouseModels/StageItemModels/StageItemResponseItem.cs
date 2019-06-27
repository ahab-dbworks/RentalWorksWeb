using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v2.Models.WarehouseModels.StageItemModels
{
    public class StageItemResponseItem
    {
        [DataType(DataType.Text)]
        public string statuscode    { get; set; }

        [DataType(DataType.Text)]
        public string statusmessage { get; set; }

        [DataType(DataType.Text)]
        public string barcode       { get; set; }

        [DataType(DataType.Text)]
        public string masteritemid  { get; set; }

        [DataType(DataType.Text)]
        public string masterid      { get; set; }

        [DataType(DataType.Text)]
        public decimal quantity     { get; set; }

        [DataType(DataType.DateTime)]
        public string datetimestamp { get; set; }
    }
}