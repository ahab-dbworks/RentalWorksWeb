using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v2.Models.OrderModels.OrderStatusDetail
{
    public class OrderStatusItemDetail
    {
        [DataType(DataType.Text)]
        public string rentalitemid { get; set; }

        [DataType(DataType.Text)]
        public string rectype      { get; set; }

        [DataType(DataType.Text)]
        public string masterid     { get; set; }

        [DataType(DataType.Text)]
        public string masteritemid { get; set; }

        [DataType(DataType.Text)]
        public string masterno     { get; set; }

        [DataType(DataType.Text)]
        public string description  { get; set; }

        [DataType(DataType.Text)]
        public string trackedby    { get; set; }

        [DataType(DataType.Text)]
        public string barcode      { get; set; }

        [DataType(DataType.Text)]
        public string serialno     { get; set; }

        [DataType(DataType.Text)]
        public string rfid         { get; set; }

        [DataType(DataType.Text)]
        public string vendor       { get; set; }

        [DataType(DataType.Text)]
        public string outdatetime  { get; set; }

        [DataType(DataType.Text)]
        public string indatetime   { get; set; }
    }
}