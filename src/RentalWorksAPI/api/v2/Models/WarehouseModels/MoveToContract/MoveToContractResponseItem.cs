namespace RentalWorksAPI.api.v2.Models.WarehouseModels.MoveToContract
{
    public class MoveToContractResponseItem
    {
        public string masteritemid  { get; set; }
        public string statuscode    { get; set; }
        public string statusmessage { get; set; }
        public string barcode       { get; set; }
        public string masterid      { get; set; }
        public int quantity         { get; set; }
        public string datetimestamp { get; set;}
    }
}