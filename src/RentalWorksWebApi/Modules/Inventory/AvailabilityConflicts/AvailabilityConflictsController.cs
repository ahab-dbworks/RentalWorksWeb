using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;

//dummy-security-controller 
namespace WebApi.Modules.Inventory.AvailabilityConflicts
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"2xsgUfsXeKJH")]
    public class AvailabilityConflictsController : AppDataController
    {
        public AvailabilityConflictsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
    }
}

