using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;

//dummy-security-controller 
namespace WebApi.Modules.Utilities.DashboardSettings
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"lXpomto7a29v")]
    public class DashboardSettingsController : AppDataController
    {
        public DashboardSettingsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
    }
}

