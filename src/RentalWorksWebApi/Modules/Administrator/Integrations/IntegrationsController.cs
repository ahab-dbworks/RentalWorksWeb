using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Administrator.Integrations
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id: "TcXQ0Mt5L0Rf")]
    public class IntegrationsController : AppDataController
    {
        public IntegrationsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(IntegrationsLogic); }
    }
}
