namespace RentalWorksAPI.api.v1.Models
{
    //----------------------------------------------------------------------------------------------------
    public class OrderNote
    {
        public string ordernoteid { get; set; }
        public string notes       { get; set; }
        public string notedate    { get; set; }
        public string notesdesc   { get; set; }
        public string datestamp   { get; set; }
        public string name        { get; set; }
        public string webusersid  { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}