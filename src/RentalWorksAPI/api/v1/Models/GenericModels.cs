using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v1.Models
{
    //----------------------------------------------------------------------------------------------------
    public class Authentication
    {
        [Required]
        [DataType(DataType.Text)]
        public string authuser     { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string authpassword { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class Error
    {
        [DataType(DataType.Text)]
        public string errno  { get; set; }

        [DataType(DataType.Text)]
        public string errmsg { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}