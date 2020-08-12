using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.Settings.InventorySettings.RetiredReason;

namespace WebApi.Modules.Mobile.AssetDisposition
{
    [Route("api/v1/quikscan/[controller]")]
    [ApiExplorerSettings(GroupName = "mobile-v1")]
    [FwController(Id:"2KbMXwEKkQe2")]
    public class AssetDispositionController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public AssetDispositionController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/quikscan/assetdisposition/lookupretirereason
        /// <summary>
        /// Get a list of valid Retired Reasons
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("lookupretiredreason")]
        [FwControllerMethod(Id: "2tD2hMWmh45C", FwControllerActionTypes.Browse)]
        public async Task<ActionResult<GetResponse<LookupRetiredReasonResponse>>> LookupRetiredReasonAsync([FromQuery] LookupRetiredReasonRequest request)
        {
            return await DoGetManyAsync<LookupRetiredReasonResponse>(request, typeof(RetiredReasonLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}
