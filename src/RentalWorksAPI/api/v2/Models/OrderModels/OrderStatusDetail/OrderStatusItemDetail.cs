namespace RentalWorksAPI.api.v2.Models.OrderModels.OrderStatusDetail
{
    public class OrderStatusItemDetail
    {
        public string rentalitemid { get; set; }
        public string rectype      { get; set; }
        public string masterid     { get; set; }
        public string masterno     { get; set; }
        public string description  { get; set; }
        public string trackedby    { get; set; }
        public string barcode      { get; set; }
        public string serialno     { get; set; }
        public string rfid         { get; set; }
        public string vendor       { get; set; }
        public string outdatetime  { get; set; }
        public string indatetime   { get; set; }
    }
}