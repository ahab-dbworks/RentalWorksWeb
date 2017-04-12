namespace RentalWorksAPI.api.v1.Models
{
    //----------------------------------------------------------------------------------------------------
    public class Invoice
    {
        public string invoiceid   { get; set; }
        public string invoiceno   { get; set; }
        public Deal deal          { get; set; }
        public string rentaltotal { get; set; }
        public string datestamp   { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}