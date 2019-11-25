using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using WebApi.Modules.Transfers.TransferOrder;

//dummy-security-controller 
namespace WebApi.Modules.Transfers.TransferOut
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"uxIAX8VBtAwD")]
    public class TransferOutController : AppDataController
    {
        public TransferOutController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/transferout/validatetransfer/browse 
        [HttpPost("validatetransfer/browse")]
        [FwControllerMethod(Id: "r2c6go3FpuIx", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateTransferBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<TransferOrderLogic>(browseRequest);
        }
    }
}

