using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Logic;
using WebApi.Modules.Warehouse.Contract;

namespace WebApi.Modules.Mobile.Exchange
{
    [Route("api/v1/quikscan/[controller]")]
    [ApiExplorerSettings(GroupName = "mobile-v1")]
    [FwController(Id:"6Ow8BBaVymuo")]
    public class ExchangeController : AppDataController
    {
        public ExchangeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/exchange/cancelcontract
        [HttpPost("cancelcontract")]
        [FwControllerMethod(Id: "w8HPRde15rzxn", ActionType: FwControllerActionTypes.Option, Caption: "Cancel Contract")]
        public async Task<ActionResult<TSpStatusResponse>> CancelContract([FromBody]CancelContractRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TSpStatusResponse response = await ContractFunc.CancelContract(AppConfig, UserSession, request);
                return response;
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
    }
}
