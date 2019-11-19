using FwStandard.AppManager;
﻿using FwCore.Controllers;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace WebApi.Modules.AccountServices.Jwt
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "accountservices-v1")]
    [FwController(Id: "fcyBsC5j7s1l")]
    public class JwtController : FwJwtController
    {
        public JwtController(IOptions<FwApplicationConfig> appConfig, ILoggerFactory loggerFactory): base(appConfig, loggerFactory)
        {

        }

        [HttpPost]
        [FwControllerMethod(Id:"v3gVmSi29OUI", AllowAnonymous:true)]
        public async Task<ActionResult<JwtResponseModel>> Post([FromBody] FwApplicationUser user)
        {
            return await DoPost(user);
        }
    }
}
