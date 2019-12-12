using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;

namespace WebApi.Modules.Mobile.Staging
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "mobile-v1")]
    [FwController(Id:"GRHqRAwrYYk0")]
    public class StagingController : AppDataController
    {
        public StagingController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
    }
}
