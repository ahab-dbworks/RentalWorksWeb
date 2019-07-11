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
    public class JwtController : FwJwtController
    {
        public JwtController(IOptions<FwApplicationConfig> appConfig, ILoggerFactory loggerFactory): base(appConfig, loggerFactory)
        {

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<JwtResponseModel>> Post([FromBody] FwApplicationUser user)
        {
            return await DoPost(user);
        }
    }
}
