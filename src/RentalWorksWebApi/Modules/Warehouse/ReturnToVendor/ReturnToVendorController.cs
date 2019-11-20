using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using WebApi.Modules.Agent.PurchaseOrder;
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
        // POST api/v1/returntovendor/validatepurchaseorder/browse 
        [HttpPost("validatepurchaseorder/browse")]
        [FwControllerMethod(Id: "Um0qP8FxPAGF", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidatePurchaseOrderBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PurchaseOrderLogic>(browseRequest);
        }
    }
}
