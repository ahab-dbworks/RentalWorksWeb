using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v1.Models
{
    public class Address
    {
        [DataType(DataType.Text)]
        public string address1 { get; set; }

        [DataType(DataType.Text)]
        public string address2 { get; set; }

        [DataType(DataType.Text)]
        public string city     { get; set; }

        [DataType(DataType.Text)]
        public string state    { get; set; }

        [DataType(DataType.Text)]
        public string zipcode  { get; set; }

        [DataType(DataType.Text)]
        public string country  { get; set; }
    }

    public class Delivery : Address
    {
        [DataType(DataType.Text)]
        public string type      { get; set; }

        [DataType(DataType.Text)]
        public string contact   { get; set; }

        [DataType(DataType.Text)]
        public string attention { get; set; }

        [DataType(DataType.Text)]
        public string phone     { get; set; }

        [DataType(DataType.Text)]
        public string location  { get; set; }
    }
}