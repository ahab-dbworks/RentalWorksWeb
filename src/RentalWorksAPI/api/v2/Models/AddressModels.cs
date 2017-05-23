namespace RentalWorksAPI.api.v2.Models
{
    //----------------------------------------------------------------------------------------------------
    public class Address
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city     { get; set; }
        public string state    { get; set; }
        public string zipcode  { get; set; }
        public string country  { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class Delivery : Address
    {
        public string type      { get; set; }
        public string contact   { get; set; }
        public string attention { get; set; }
        public string phone     { get; set; }
        public string location  { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}