using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using WebApi.Modules.Agent.PurchaseOrder;
using WebApi.Modules.Warehouse.Contract;

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
        // POST api/v1/assignbarcodes/validatepurchaseorder/browse 
        [HttpPost("validatepurchaseorder/browse")]
        [FwControllerMethod(Id: "Jkv2n1qkBTTP", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidatePurchaseOrderBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PurchaseOrderLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/assignbarcodes/validatecontract/browse 
        [HttpPost("validatecontract/browse")]
        [FwControllerMethod(Id: "fRe2rwvdpdV0", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateContractBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ContractLogic>(browseRequest);
        }
    }
}

