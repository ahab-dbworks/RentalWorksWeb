using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v1.Models
{
    //----------------------------------------------------------------------------------------------------
    public class Invoice
    {
        [DataType(DataType.Text)]
        public string invoiceid   { get; set; }

        [DataType(DataType.Text)]
        public string invoiceno   { get; set; }

        public Deal deal          { get; set; }

        [DataType(DataType.Text)]
        public string rentaltotal { get; set; }

        [DataType(DataType.DateTime)]
        public string datestamp   { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}