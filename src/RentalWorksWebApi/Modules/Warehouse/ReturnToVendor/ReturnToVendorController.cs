using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
//dummy-security-controller 

namespace WebApi.Modules.Warehouse.ReturnToVendor
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"cCxoTvTCDTcm")]
    public class ReturnToVendorController : AppDataController
    {
        public ReturnToVendorController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
    }
}
