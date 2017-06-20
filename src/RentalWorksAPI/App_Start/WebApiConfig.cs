using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using RentalWorksAPI.Filters;
using RentalWorksAPI.Routing;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;

namespace RentalWorksAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.Filters.Add(new AppConfigAuthorizeAttribute());

            config.EnableCors();
            config.MessageHandlers.Add(new PreflightRequestHandler());

            var constraintsResolver = new DefaultInlineConstraintResolver(); 
            constraintsResolver.ConstraintMap.Add("apiVersion1Constraint", typeof(RwAPIv1Constraint)); 
            constraintsResolver.ConstraintMap.Add("apiVersion2Constraint", typeof(RwAPIv2Constraint)); 

            // Web API routes
            //config.MapHttpAttributeRoutes();
            config.MapHttpAttributeRoutes(constraintsResolver); 
            config.Services.Replace(typeof(IHttpControllerSelector), new NamespaceHttpControllerSelector(config));

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            config.Routes.MapHttpRoute(
                name: "RentalWorksWebApi",
                routeTemplate: "appapi/v1/{controller}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore};
        }
    }
}
