using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Utilities.CurrencyMissingUtility
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "Bv2Gucza8DSAf")]
    public class CurrencyMissingUtilityController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public CurrencyMissingUtilityController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) {  }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/currencymissingutility/applyproposedcurrencies
        [HttpPost("applyproposedcurrencies")]
        [FwControllerMethod(Id: "pKOAzRfYRSW5Y", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<ApplyProposedCurrenciesResponse>> ApplyProposedCurrenciesAsync(ApplyProposedCurrenciesRequest request)
        {
            return await CurrencyMissingUtilityFunc.ApplyProposedCurrencies(AppConfig, UserSession, request);
        }
        //------------------------------------------------------------------------------------ 
    }
}
