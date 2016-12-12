using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v1.Models
{
    //----------------------------------------------------------------------------------------------------
    public class Authentication
    {
        [Required]
        public string authuser     { get; set; }
        [Required]
        public string authpassword { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class Error
    {
        public string errno  { get; set; }
        public string errmsg { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}