using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace FwCore.Controllers
{
    [Route("api/v1/[controller]")]
    public class FwController : Controller  //todo: create FwController to inherit from
    {
        protected readonly FwApplicationConfig AppConfig;
        //------------------------------------------------------------------------------------
        protected FwUserSession UserSession { get; set; }
        //------------------------------------------------------------------------------------
        public FwController(IOptions<FwApplicationConfig> appConfig)
        {
            this.AppConfig = appConfig.Value;
        }
        //------------------------------------------------------------------------------------
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            this.UserSession = new FwUserSession(this.User);
            return base.OnActionExecutionAsync(context, next);
        }
        //------------------------------------------------------------------------------------
    }
}