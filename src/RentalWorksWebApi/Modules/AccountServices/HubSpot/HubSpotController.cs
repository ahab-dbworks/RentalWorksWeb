using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.AccountServices.HubSpot
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id: "h2RpvAvlkDLA")]
    public class HubSpotController : AppDataController
    {
        public HubSpotController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(HubSpotLogic); }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/hubspot/allcontacts
        [HttpPost("allcontacts")]
        [FwControllerMethod(Id: "tXvIp7gCbP5B", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<string>> GetAsync([FromBody]GetHubSpotContactsRequest l)
        {
            var hs = FwBusinessLogic.CreateBusinessLogic<HubSpotLogic>(this.AppConfig, this.UserSession);
            return await hs.GetContactsAsync(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/hubspot/newcontact 
        [HttpPost("newcontact")]
        [FwControllerMethod(Id: "bfAAIzIssQIC", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<string>> NewAsync([FromBody]PostHubSpotContactRequest l)
        {
            var hs = FwBusinessLogic.CreateBusinessLogic<HubSpotLogic>(this.AppConfig, this.UserSession);
            return await hs.PostContactAsync(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/hubspot/gettokens 
        [HttpPost("gettokens")]
        [FwControllerMethod(Id: "D6Jh3OAuOSTL", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<GetWriteTokensResponse>> PostAsync([FromBody]GetHubSpotTokensRequest l)
        {
            var hs = FwBusinessLogic.CreateBusinessLogic<HubSpotLogic>(this.AppConfig, this.UserSession);
            return await hs.GetTokensAsync(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/hubspot/getcontactsepoch 
        [HttpPost("getcontactsepoch")]
        [FwControllerMethod(Id: "abSnMnaW8gLP", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<dynamic>> GetContactsEpochAsync([FromBody]SearchHubSpotContactsWithinPeriodRequest l)
        {
            var hs = FwBusinessLogic.CreateBusinessLogic<HubSpotLogic>(this.AppConfig, this.UserSession);
            return await hs.SearchContactsWithinPeriodAsync(l);
        }
    }

}
