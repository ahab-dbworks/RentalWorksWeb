using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace FwCore.Controllers
{
    public class FwController : Controller
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