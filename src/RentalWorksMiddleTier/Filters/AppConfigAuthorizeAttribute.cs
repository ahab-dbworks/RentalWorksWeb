using Fw.Json.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace RentalWorksMiddleTier.Filters
{
    public class AppConfigAuthorizeAttribute : AuthorizationFilterAttribute
    {
        public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new { message = "Authorization has been denied for this request." });
            }
            else
            {
                byte[] data          = Convert.FromBase64String(actionContext.Request.Headers.Authorization.Parameter);
                string decodedString = Encoding.UTF8.GetString(data);
                string[] authStrings = decodedString.Split(':');

                if ((authStrings[0] != FwApplicationConfig.CurrentSite.APISettings.AuthUsername) ||
                    (authStrings[1] != FwApplicationConfig.CurrentSite.APISettings.AuthPassword))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new { message = "Authorization has been denied for this request." });
                }
            }

            return Task.FromResult<object>(null);
        }
    }
}