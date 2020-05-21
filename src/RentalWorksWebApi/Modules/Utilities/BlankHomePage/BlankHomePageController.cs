using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using FwStandard.AppManager;

namespace WebApi.Modules.Utilities.BlankHomePage
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "OCtX2qmSedHfq")]
    public class BlankHomePageController : AppDataController
    {
        public BlankHomePageController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = null; }
        //------------------------------------------------------------------------------------ 
    }
}
