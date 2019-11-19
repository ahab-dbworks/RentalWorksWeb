using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;

//dummy-security-controller 
namespace WebApi.Modules.Warehouse.AssignBarCodes
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"7UU96BApz2Va")]
    public class AssignBarCodesController : AppDataController
    {
        public AssignBarCodesController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
    }
}

