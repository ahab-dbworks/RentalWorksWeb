using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.AccountServices.HubSpot;

namespace WebApi.Modules.Administrator.Integrations
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id: "TcXQ0Mt5L0Rf")]
    public class IntegrationsController : AppDataController
    {
        public IntegrationsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(IntegrationsLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/integrations/hashubspotrefreshtoken 
        [HttpPost("hashubspotrefreshtoken")]
        [FwControllerMethod(Id: "WJK6FvyaVlHQ", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<GetHubSpotRefreshTokenBool>> PostAsync()
        {
            var hs = FwBusinessLogic.CreateBusinessLogic<HubSpotLogic>(this.AppConfig, this.UserSession);
            return await hs.GetRefreshTokenBoolAsync();
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/integrations/deletehubspottokens 
        [HttpPost("deletehubspottokens")]
        [FwControllerMethod(Id: "7VELVFCxfYWz", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<DeleteHubSpotTokens>> DeleteAsync()
        {
            var hs = FwBusinessLogic.CreateBusinessLogic<HubSpotLogic>(this.AppConfig, this.UserSession);
            return await hs.DeleteTokensAsync();
        }
    }
}
