using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;

namespace WebApi.Modules.Mobile.Repair
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "mobile-v1")]
    [FwController(Id:"etbWAOdN0p1J")]
    public class RepairController : AppDataController
    {
        public RepairController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
    }
}
