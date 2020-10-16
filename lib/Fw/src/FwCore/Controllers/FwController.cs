using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
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
            Thread.CurrentPrincipal = this.User;
            return base.OnActionExecutionAsync(context, next);
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Uses the request to get the fully qualified base url without a trailing backslash
        /// </summary>
        /// <returns></returns>
        protected string GetFullyQualifiedBaseUrl()
        {
            string appPath = this.AppConfig.PublicBaseUrl;

            if (string.IsNullOrEmpty(appPath))
            {
                //Formatting the fully qualified website url/name
                appPath = string.Format("{0}://{1}{2}{3}",
                            HttpContext.Request.Scheme,
                            HttpContext.Request.Host.Host,
                            (HttpContext.Request.Host.Port == null || (HttpContext.Request.Scheme == "http" && HttpContext.Request.Host.Port == 80) || (HttpContext.Request.Scheme == "https" && HttpContext.Request.Host.Port == 443)) ? string.Empty : ":" + HttpContext.Request.Host.Port,
                            this.AppConfig.VirtualDirectory);
            }
            if (appPath.EndsWith("/"))
            {
                appPath = appPath.Substring(0, appPath.Length - 1);
            }

            return appPath;
        }
        //------------------------------------------------------------------------------------
    }
}