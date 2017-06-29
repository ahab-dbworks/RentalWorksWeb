using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;
using RentalWorksWebApi;
using System.Collections.Generic;

namespace RentalWorksCoreApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class RwController : Controller  //todo: create FwController to inherit from
    {
        protected readonly ApplicationConfig _appConfig;
        //------------------------------------------------------------------------------------
        public RwController(IOptions<ApplicationConfig> appConfig)
        {
            _appConfig = appConfig.Value;
        }
        //------------------------------------------------------------------------------------
    }
}