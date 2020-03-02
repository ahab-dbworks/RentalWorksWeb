using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;

namespace WebApi.Modules.Utilities.InventorySequenceUtility
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "NY5nvYtS0WnEj")]
    public class InventorySequenceUtilityController : AppDataController
    {
        public InventorySequenceUtilityController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }

    }
}