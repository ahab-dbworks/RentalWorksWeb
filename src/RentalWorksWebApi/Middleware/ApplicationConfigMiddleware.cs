using FwStandard.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebApi.Middleware
{
    /// <summary>
    /// Dynamically generates the applicationconfig.js file from appsettings.json
    /// </summary>
    class ApplicationConfigMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly FwApplicationConfig _appConfig;
        private readonly Regex _regexWebProdApplicationConfig;
        private readonly Regex _regexWebDevApplicationConfig = new Regex("^.*\\/webdev\\/applicationconfig.js$");
        private readonly Regex _regexMobileProdApplicationConfig;
        private readonly Regex _regexMobileDevApplicationConfig = new Regex("^.*\\/quikscandev\\/applicationconfig.js$");
        private readonly string _version = "0.0.0.0";

        public ApplicationConfigMiddleware(RequestDelegate next, IOptions<FwApplicationConfig> options)
        {
            this._next = next;
            this._appConfig = options.Value;
            string webPathRoot = this._appConfig.WebRequestPath;
            //if (string.IsNullOrEmpty(webPathRoot))
            //{
            //    webPathRoot = "/";
            //}
            this._regexWebProdApplicationConfig = new Regex("^.*\\" + webPathRoot + "/applicationconfig.js$");

            string mobilePathRoot = this._appConfig.MobileRequestPath;
            //if (string.IsNullOrEmpty(mobilePathRoot))
            //{
            //    mobilePathRoot = "/";
            //}
            this._regexMobileProdApplicationConfig = new Regex("^.*\\" + mobilePathRoot + "/applicationconfig.js$");
            
            string pathVersion = Path.Combine(Environment.CurrentDirectory, "version.txt");
            if (File.Exists(pathVersion))
            {
                this._version = File.ReadAllText(pathVersion);
            }
        }

        public async Task Invoke(HttpContext httpContext) {
            
            try
            {
                // dynamically generate QuikScan's ApplicationConfig.js
                string path = httpContext.Request.Path.Value.ToLower();
                if (this._appConfig.WebApp != null && (_regexWebProdApplicationConfig.IsMatch(path) || _regexWebDevApplicationConfig.IsMatch(path)))
                {
                    WebAppConfig webAppConfig = JsonConvert.DeserializeObject<WebAppConfig>(JsonConvert.SerializeObject(this._appConfig.WebApp));
                    if (_appConfig.PublicBaseUrl != null && _appConfig.PublicBaseUrl.Length > 0)
                    {
                        webAppConfig.apiurl = this._appConfig.PublicBaseUrl;
                    }
                    webAppConfig.version = this._version;

                    string jsonAppConfig = JsonConvert.SerializeObject(webAppConfig);
                    httpContext.Response.StatusCode = 200;
                    await httpContext.Response.WriteAsync($"applicationConfig = JSON.parse('{jsonAppConfig}');\n");
                    return;
                }

                // dynamically generate Web's ApplicationConfig.js
                if (this._appConfig.MobileApp != null && (_regexMobileProdApplicationConfig.IsMatch(path) || _regexMobileDevApplicationConfig.IsMatch(path)))
                {
                    MobileAppConfig mobileAppConfig = JsonConvert.DeserializeObject<MobileAppConfig>(JsonConvert.SerializeObject(this._appConfig.MobileApp));
                    if (_appConfig.PublicBaseUrl != null && _appConfig.PublicBaseUrl.Length > 0)
                    {
                        mobileAppConfig.apiurl = this._appConfig.PublicBaseUrl;
                    }
                    mobileAppConfig.version = this._version;
                    string jsonAppConfig = JsonConvert.SerializeObject(mobileAppConfig);
                    httpContext.Response.StatusCode = 200;
                    await httpContext.Response.WriteAsync($"applicationConfig = JSON.parse('{jsonAppConfig}');\n");
                    return;
                }
                await _next(httpContext); // Call the pipeline
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = 500;
                FwApiException apiException = new FwApiException();
                apiException.StatusCode = 500;
                apiException.Message = ex.Message;
                apiException.StackTrace = ex.StackTrace;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(apiException));
                return;
            }
        }
    }
    
    public static class ApplicationConfigMiddlewareExtensions
    {
        public static IApplicationBuilder ApplicationConfigMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApplicationConfigMiddleware>();
        }
    }
}
