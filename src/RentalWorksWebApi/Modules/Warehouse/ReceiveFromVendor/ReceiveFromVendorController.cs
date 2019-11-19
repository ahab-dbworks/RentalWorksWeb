using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
//dummy-security-controller 

namespace WebApi.Modules.Warehouse.ReceiveFromVendor
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"MtgBxCKWVl7m")]
    public class ReceiveFromVendorController : AppDataController
    {
        public ReceiveFromVendorController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
    }
}
