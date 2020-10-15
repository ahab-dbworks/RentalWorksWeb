﻿using FwStandard.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebApi.Middleware
{
    /// <summary>
    /// Dynamically generates the applicationconfig.js file from appsettings.json
    /// </summary>
    class ApplicationConfigMiddleware
    {
        private const string RENTALWORKS = "rentalworks";
        private const string TRAKITWORKS = "trakitworks";
        private const string QUIKSCAN = "quikscan";
        private readonly RequestDelegate _next;
        private readonly FwApplicationConfig _appConfig;
        private readonly Regex _regexRentalWorksProdApplicationConfig = null;
        private readonly Regex _regexRentalWorksDevApplicationConfig = null;
        private readonly Regex _regexTrakItWorksProdApplicationConfig = null;
        private readonly Regex _regexTrakItWorksDevApplicationConfig = null;
        private readonly Regex _regexQuikScanProdApplicationConfig = null;
        private readonly Regex _regexQuikScanDevApplicationConfig = null;
        private readonly string _version = "0.0.0.0";

        public ApplicationConfigMiddleware(RequestDelegate next, IOptions<FwApplicationConfig> options)
        {
            this._next = next;
            this._appConfig = options.Value;

            if (this._appConfig.Apps.ContainsKey(RENTALWORKS))
            {
                string rentalworkVirtualDirectory = this._appConfig.Apps[RENTALWORKS].VirtualDirectory;
                this._regexRentalWorksProdApplicationConfig = new Regex($"^{rentalworkVirtualDirectory}/applicationconfig.js$");
                this._regexRentalWorksDevApplicationConfig = new Regex("^\\/webdev\\/applicationconfig.js$");
            }

            if (this._appConfig.Apps.ContainsKey(TRAKITWORKS))
            {
                string trakitworksVirtualDirectory = this._appConfig.Apps[TRAKITWORKS].VirtualDirectory;
                this._regexTrakItWorksProdApplicationConfig = new Regex($"^{trakitworksVirtualDirectory}/applicationconfig.js$");
                this._regexTrakItWorksDevApplicationConfig = new Regex("^\\/trakitworksdev\\/applicationconfig.js$");
            }

            if (this._appConfig.Apps.ContainsKey(QUIKSCAN))
            {
                string quikscanVirtualDirectory = this._appConfig.Apps[QUIKSCAN].VirtualDirectory;
                this._regexQuikScanProdApplicationConfig = new Regex($"^{quikscanVirtualDirectory}/applicationconfig.js$");
                this._regexQuikScanDevApplicationConfig = new Regex("^\\/quikscandev\\/applicationconfig.js$");
            }
            
            string pathVersion = Path.Combine(Environment.CurrentDirectory, "version.txt");
            if (File.Exists(pathVersion))
            {
                this._version = File.ReadAllText(pathVersion);
            }
        }

        public async Task Invoke(HttpContext httpContext) {
            
            try
            {
                // dynamically generate Web's ApplicationConfig.js
                string path = httpContext.Request.Path.Value.ToLower();
                if (this._appConfig.Apps.ContainsKey(RENTALWORKS) && (_regexRentalWorksProdApplicationConfig.IsMatch(path) || _regexRentalWorksDevApplicationConfig.IsMatch(path)))
                {
                    WebAppConfig appConfig = JsonConvert.DeserializeObject<WebAppConfig>(JsonConvert.SerializeObject(this._appConfig.Apps[RENTALWORKS].Config));
                    if (_appConfig.PublicBaseUrl != null && _appConfig.PublicBaseUrl.Length > 0)
                    {
                        appConfig.apiurl = this._appConfig.PublicBaseUrl;
                    }
                    //webAppConfig.version = this._version;

                    httpContext.Response.StatusCode = 200;
                    await httpContext.Response.WriteAsync(this.GetApplicationConfigText(appConfig));
                    return;
                }

                if (this._appConfig.Apps.ContainsKey(TRAKITWORKS) && (_regexTrakItWorksProdApplicationConfig.IsMatch(path) || _regexTrakItWorksDevApplicationConfig.IsMatch(path)))
                {
                    WebAppConfig appConfig = JsonConvert.DeserializeObject<WebAppConfig>(JsonConvert.SerializeObject(this._appConfig.Apps[TRAKITWORKS].Config));
                    if (_appConfig.PublicBaseUrl != null && _appConfig.PublicBaseUrl.Length > 0)
                    {
                        appConfig.apiurl = this._appConfig.PublicBaseUrl;
                    }
                    //webAppConfig.version = this._version;

                    httpContext.Response.StatusCode = 200;
                    await httpContext.Response.WriteAsync(this.GetApplicationConfigText(appConfig));
                    return;
                }

                // dynamically generate QuikScan's ApplicationConfig.js
                if (this._appConfig.Apps.ContainsKey(QUIKSCAN) && (_regexQuikScanProdApplicationConfig.IsMatch(path) || _regexQuikScanDevApplicationConfig.IsMatch(path)))
                {
                    MobileAppConfig appConfig = JsonConvert.DeserializeObject<MobileAppConfig>(JsonConvert.SerializeObject(this._appConfig.Apps[QUIKSCAN].Config));
                    if (_appConfig.PublicBaseUrl != null && _appConfig.PublicBaseUrl.Length > 0)
                    {
                        appConfig.apiurl = this._appConfig.PublicBaseUrl;
                    }
                    //mobileAppConfig.version = this._version;
                    
                    httpContext.Response.StatusCode = 200;
                    await httpContext.Response.WriteAsync(this.GetApplicationConfigText(appConfig));
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

        string GetApplicationConfigText(object configObj)
        {
            // create a JSON serializer that ignores nulls, so we can strip all the unassigned properties out
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            serializerSettings.NullValueHandling = NullValueHandling.Ignore;
            JsonSerializer serializer = JsonSerializer.Create(serializerSettings);
            StringBuilder jsonAppConfig = new StringBuilder();
            using (StringWriter writer = new StringWriter(jsonAppConfig))
            {
                serializer.Serialize(writer, configObj);
            }

            // populate a dictionary with the stripped down JSON object
            Dictionary<string, object> appConfigObj = new Dictionary<string, object>();
            JsonConvert.PopulateObject(jsonAppConfig.ToString(), appConfigObj);
            
            // generate the applicationConfig file
            StringBuilder sb = new StringBuilder();
            foreach (var item in appConfigObj)
            {
                if (item.Value is string)
                {
                    sb.AppendLine($"applicationConfig.{item.Key} = '{item.Value}';");
                }
                else if (item.Value is bool)
                {
                    sb.AppendLine($"applicationConfig.{item.Key} = {item.Value.ToString().ToLower()};");
                }
                else
                {
                    sb.AppendLine($"applicationConfig.{item.Key} = {item.Value};");
                }
            }
            return sb.ToString();
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
