using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v1.Models
{
    //----------------------------------------------------------------------------------------------------
    public class Login
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string email    { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string password { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class ChangePassword
    {
        [Required]
        [DataType(DataType.Text)]
        public string webusersid { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string password   { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class ResetPassword
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}