using FwStandard.AppManager;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WebApi.Modules.AccountServices.Account
{
    //------------------------------------------------------------------------------------
    public class ResetPasswordRequest
    {
        [Required]
        public string Password { get; set; }
    }
    //------------------------------------------------------------------------------------
    public class ResetPasswordResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
    }

    public class SystemSettingsResponse
    {
        public bool QuikScanStageBySession { get; set; }
        public FwJsonDataTable BarcodeSkipPrefixes { get; set; }
    }
    //------------------------------------------------------------------------------------
}
