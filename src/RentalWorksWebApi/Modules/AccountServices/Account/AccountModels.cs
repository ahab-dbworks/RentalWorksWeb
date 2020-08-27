using FwStandard.AppManager;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WebApi.Modules.AccountServices.Account
{
    //------------------------------------------------------------------------------------
    public class ResetPasswordRequest
    {
        [StringLength(8)]
        public string WebUsersId { get; set; }
        [StringLength(8)]
        public string UsersId { get; set; }
        [StringLength(8)]
        public string ContactId { get; set; }
        [FwStringRange(IsRequired: true, AllowableValues: new [] { "USER", "CONTACT" })]
        public string UserType { get; set; }
        [Required]
        public string Password { get; set; }
        //------------------------------------------------------------------------------------
        public async Task<bool> ValidateAsync(ModelStateDictionary modelState)
        {
            bool valid = true;

            if (string.IsNullOrEmpty(WebUsersId))
            {
                modelState.AddModelError("WebUsersId", "WebUsersId is required.");
                valid = false;
            }
            if (string.IsNullOrEmpty(UsersId))
            {
                modelState.AddModelError("UsersId", "UsersId is required.");
                valid = false;
            }

            if (UserType == "CONTACT")
            {
                if (string.IsNullOrEmpty(ContactId))
                {
                    modelState.AddModelError("ContactId", "ContactId is required.");
                    valid = false;
                }
            }

            await Task.CompletedTask;
            return valid;
        }
        //------------------------------------------------------------------------------------
    }
    //------------------------------------------------------------------------------------
    public class ResetPasswordResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
    }
    //------------------------------------------------------------------------------------
}
