using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v1.Models
{
    //----------------------------------------------------------------------------------------------------
    public class Login
    {
        [Required]
        public string email    { get; set; }
        [Required]
        public string password { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class ChangePassword
    {
        [Required]
        public string webusersid { get; set; }
        [Required]
        public string password   { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class ResetPassword
    {
        [Required]
        public string email { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}