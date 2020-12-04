using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.AccountServices.HubSpot;

namespace WebApi.Modules.Plugins.Hubspot
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "plugins-v1")]
    [FwController(Id: "sLWtkElkRFDW")]
    public class HubspotPluginController : AppDataController
    {
        public HubspotPluginController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(HubSpotLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/hubspotplugin/hashubspotrefreshtoken 
        [HttpPost("hashubspotrefreshtoken")]
        [FwControllerMethod(Id: "WJK6FvyaVlHQ", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<GetHubSpotRefreshTokenBool>> PostAsync()
        {
            var hs = FwBusinessLogic.CreateBusinessLogic<HubSpotLogic>(this.AppConfig, this.UserSession);
            return await hs.GetRefreshTokenBoolAsync();
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/hubspotplugin/deletehubspottokens 
        [HttpPost("deletehubspottokens")]
        [FwControllerMethod(Id: "7VELVFCxfYWz", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<DeleteHubSpotTokens>> DeleteAsync()
        {
            var hs = FwBusinessLogic.CreateBusinessLogic<HubSpotLogic>(this.AppConfig, this.UserSession);
            return await hs.DeleteTokensAsync();
        }
        //------------------------------------------------------------------------------------ 
    }
}
