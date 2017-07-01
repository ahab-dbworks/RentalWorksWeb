namespace RentalWorksAPI.api.v2.Models.WarehouseModels.UnstageItemModels
{
    public class UnstageItemResponseItem
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