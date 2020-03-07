using FwStandard.AppManager;
﻿using FwCore.Controllers;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using FwCore.Logic;

namespace WebApi.Modules.AccountServices.Jwt
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "accountservices-v1")]
    [FwController(Id: "fcyBsC5j7s1l")]
    public class JwtController : FwJwtController
    {
        //---------------------------------------------------------------------------------------------
        public JwtController(IOptions<FwApplicationConfig> appConfig, ILoggerFactory loggerFactory): base(appConfig, loggerFactory)
        {

        }
        //---------------------------------------------------------------------------------------------
        [HttpPost]
        [FwControllerMethod(Id:"v3gVmSi29OUI", AllowAnonymous:true)]
        public async Task<ActionResult<JwtResponseModel>> Post([FromBody] FwApplicationUser user)
        {
            return await DoPost(user);
        }
        //---------------------------------------------------------------------------------------------
        [HttpPost("okta")]
        [FwControllerMethod(Id: "pIEMqgu0mQQp", FwControllerActionTypes.Browse, AllowAnonymous: true, ValidateSecurityGroup: false)]
        public async Task<ActionResult<JwtResponseModel>> OktaPost([FromBody] OktaRequest request)
        {
            return await DoOktaPost(request);
        }
        //---------------------------------------------------------------------------------------------
        [HttpPost("oktaverify")]
        [FwControllerMethod(Id: "O9LIBJ0ssg8d", FwControllerActionTypes.Browse, AllowAnonymous: true, ValidateSecurityGroup: false)]
        public async Task<ActionResult<OktaSessionResponseModel>> OktaVerify([FromBody] OktaSessionRequest request)
        {
            request.appConfig = this._appConfig;
            return await FwJwtLogic.OktaVerifySession(request);
        }
        //---------------------------------------------------------------------------------------------
    }
}
