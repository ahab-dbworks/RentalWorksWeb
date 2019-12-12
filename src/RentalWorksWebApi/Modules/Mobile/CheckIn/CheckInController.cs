using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;

namespace WebApi.Modules.Mobile.CheckIn
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "mobile-v1")]
    [FwController(Id:"S2kfsHmsu1oO")]
    public class CheckInController : AppDataController
    {
        public CheckInController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
    }
}
