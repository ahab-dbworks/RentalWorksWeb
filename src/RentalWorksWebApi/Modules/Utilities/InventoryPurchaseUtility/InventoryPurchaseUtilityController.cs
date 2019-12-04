using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;

namespace WebApi.Modules.Utilities.InventoryPurchaseUtility
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "sOxbXBmCPc9y")]
    public class InventoryPurchaseUtilityController : AppDataController
    {
        public InventoryPurchaseUtilityController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) {  }
        //------------------------------------------------------------------------------------ 
    }
}
