namespace RentalWorksAPI.api.v2.Models.WarehouseModels.StageItemModels
{
    public class StageItemResponseItem
    {
        public string statuscode    { get; set; }
        public string statusmessage { get; set; }
        public string barcode       { get; set; }
        public string masteritemid  { get; set; }
        public string masterid      { get; set; }
        public decimal quantity     { get; set; }
        public string datetimestamp { get; set; }
    }
}