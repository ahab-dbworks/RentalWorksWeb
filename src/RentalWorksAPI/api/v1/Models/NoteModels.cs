using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v1.Models
{
    //----------------------------------------------------------------------------------------------------
    public class OrderNote
    {
        [DataType(DataType.Text)]
        public string ordernoteid { get; set; }

        [DataType(DataType.Text)]
        public string notes       { get; set; }

        [DataType(DataType.Text)]
        public string notedate    { get; set; }

        [DataType(DataType.Text)]
        public string notesdesc   { get; set; }

        [DataType(DataType.Text)]
        public string datestamp   { get; set; }

        [DataType(DataType.Text)]
        public string name        { get; set; }

        [DataType(DataType.Text)]
        public string webusersid  { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}