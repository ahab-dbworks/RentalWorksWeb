using FwCore.Controllers;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    public abstract class AppDataController : FwDataController  
    {
        public AppDataController(IOptions<FwApplicationConfig> appConfig) :  base(appConfig)
        {

        }
    }
}