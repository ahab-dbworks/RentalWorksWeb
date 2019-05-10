using FwCore.Controllers;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class AppController : FwController
    {
        public AppController(IOptions<FwApplicationConfig> appConfig) : base(appConfig)
        {

        }
    }
}