using AutoMapper;
using FwCore.AppManager;
using FwCore.Middleware;
using FwStandard.Models;
using FwStandard.Modules.Administrator.Alert;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OfficeOpenXml.ConditionalFormatting;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FwCore.Api
{
    public abstract class FwStartup
    {
        //------------------------------------------------------------------------------------
        protected IHostingEnvironment HostingEnvironment;
        protected IConfigurationRoot Configuration;
        protected FwApplicationConfig ApplicationConfig;
        public static string SystemName;
        //------------------------------------------------------------------------------------
        public FwStartup(IHostingEnvironment env, string systemName)
        {
            HostingEnvironment = env;
            SystemName = systemName;
            FwSqlConnection.ApplicationName = systemName;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            Mapper.Initialize(config =>
            {
                config.CreateMissingTypeMaps = true;
                config.ValidateInlineMaps = false;
            });
        }
        //------------------------------------------------------------------------------------
        public virtual void ConfigureServices(IServiceCollection services)
        {
            ApplicationConfig = Configuration.GetSection("ApplicationConfig").Get<FwApplicationConfig>();

            if (string.IsNullOrEmpty(ApplicationConfig.PublicBaseUrl))
            {
                throw new Exception("ApplicationConfig.PublicBaseUrl must be set in appsettings.json, see appsettings.sample.json for an example.");
            }
            if (string.IsNullOrEmpty(ApplicationConfig.JwtIssuerOptions.SecretKey))
            {
                throw new Exception("ApplicationConfig.JwtIssuerOptions.SecretKey must be set in appsettings.json, here's a randomly generated key you can use: " + Guid.NewGuid().ToString().Replace("-", "").ToUpper() + Guid.NewGuid().ToString().Replace("-", "").ToUpper());
            }
            if (string.IsNullOrEmpty(ApplicationConfig.JwtIssuerOptions.Issuer))
            {
                throw new Exception("ApplicationConfig.JwtIssuerOptions.Issuer must be set in appsettings.json, see appsettings.sample.json for an example.");
            }
            if (string.IsNullOrEmpty(ApplicationConfig.JwtIssuerOptions.Authority))
            {
                throw new Exception("ApplicationConfig.JwtIssuerOptions.Authority must be set in appsettings.json, see appsettings.sample.json for an example.");
            }

            FwSqlLogEntry.LogSql = this.ApplicationConfig.Debugging.LogSql;
            FwSqlLogEntry.LogSqlContext = this.ApplicationConfig.Debugging.LogSqlContext;

            services.AddCors();

            services
                .AddOptions()
                .Configure<FwApplicationConfig>(Configuration.GetSection("ApplicationConfig"))
                .AddMvc(config =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        //.AddRequirements(new IAuthorizationRequirement[] { new FwAppManagerAuthorizationRequirement() })
                        .Build();
                    //config.Filters.Add(new AuthorizeFilter(policy));
                    config.Filters.Insert(0, new FwAmAuthorizationFilter());
                })
                // don't use the lowerCamelCase formatter
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new FwContractResolver();
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                })
            ;

            //require HTTPS in production configuration
            //if (HostingEnvironment.IsProduction())
            //{
            //    services.Configure<MvcOptions>(options =>
            //    {
            //        options.Filters.Add(new RequireHttpsAttribute());
            //    });
            //}

            // Require Security Tree Authorization for every controller method
            services.AddAuthorization(options =>
            {
                // setup the default authorization policy to require jwt bearer authentication for every controller method
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();

                // add an authorization policy for every controller method in the WebApi security tree
                //FwSecurityTreeNode nodeCurrentApplication = FwSecurityTree.Tree.FindById(FwSecurityTree.CurrentApplicationId);
                //// loop over each api top level menu
                //foreach (var lv1menu in nodeCurrentApplication.Children)
                //{
                //    // loop over each controller
                //    foreach (var controller in lv1menu.Children)
                //    {
                //        // loop over each controller method
                //        foreach (var method in controller.Children)
                //        {
                //            // Adds a callback that will fire with each request on the controller method to validate whether the user has permission
                //            options.AddPolicy("{" + method.Id + "}", policy => policy.AddRequirements(new SecurityTreeAuthorizationRequirement(method.Id, true, false)));
                //        }
                //    }
                //}

                //options.AddPolicy("AppManager", policy => policy.AddRequirements(new FwAmAuthorizationRequirement("", true, false)));
            });
            //services.AddSingleton<IAuthorizationHandler, FwAmAuthorizationHandler>();

            // Configure JwtIssuerOptions for dependency injection
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ApplicationConfig.JwtIssuerOptions.SecretKey));
            services.Configure<FwJwtIssuerOptions>(options =>
            {
                options.Authority = ApplicationConfig.JwtIssuerOptions.Authority;
                options.Issuer = ApplicationConfig.JwtIssuerOptions.Issuer;
                options.Audience = ApplicationConfig.JwtIssuerOptions.Audience;
            });

            // this is the new ASP.NET Core 2.0 stuff for JWT (not sure about any other JWT stuff in this file anymore)
            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = ApplicationConfig.JwtIssuerOptions.Issuer,

                        ValidateAudience = true,
                        ValidAudience = ApplicationConfig.JwtIssuerOptions.Audience,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingKey,

                        RequireExpirationTime = false,
                        ValidateLifetime = false

                        //ClockSkew = TimeSpan.Zero
                    };

                })
            ;

            services.AddSwaggerGen(c =>
            {
                c.DescribeAllEnumsAsStrings();
                this.AddSwaggerDocs(c);
                c.OperationFilter<FwFormFileSwaggerFilter>();
                var filePath = Path.Combine(ApplicationEnvironment.ApplicationBasePath, "FwStandard.xml");
                c.IncludeXmlComments(filePath);
                filePath = Path.Combine(ApplicationEnvironment.ApplicationBasePath, "FwCore.xml");
                c.IncludeXmlComments(filePath);
                filePath = Path.Combine(ApplicationEnvironment.ApplicationBasePath, "WebApi.xml");
                c.IncludeXmlComments(filePath);
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new[] { "readAccess", "writeAccess" } }
                });

                // rename the models
                c.CustomSchemaIds((type) => {
                    string modelName = FwTypeTranslator.GetFriendlyName(type);
                    if (modelName.StartsWith("WebApi") && modelName.EndsWith("Logic"))
                    {
                        modelName = modelName.Substring(0, modelName.Length - 5) + "Model";
                    }
                    return modelName;
                });
            });

            //if (FwSqlSelect.PagingCompatibility == FwSqlSelect.PagingCompatibilities.AutoDetect)
            //{
            //    using (FwSqlConnection conn = new FwSqlConnection(ApplicationConfig.DatabaseSettings.ConnectionString))
            //    {
            //        bool isGte = FwSqlData.IsSqlVersionGreaterThanOrEqualTo(conn, ApplicationConfig.DatabaseSettings, 2012).Result;
            //        if (isGte)
            //        {
            //            FwSqlSelect.PagingCompatibility = FwSqlSelect.PagingCompatibilities.Sql2012;
            //        }
            //        else
            //        {
            //            FwSqlSelect.PagingCompatibility = FwSqlSelect.PagingCompatibilities.PreSql2012;
            //        }
            //    }
            //}
            if (this.ApplicationConfig.DatabaseSettings.SQLCompatibility == "PreSql2012")
            {
                FwSqlSelect.PagingCompatibility = FwSqlSelect.PagingCompatibilities.PreSql2012;
                Console.WriteLine("SqlSelect Paging Compatibility: PreSql2012");
            }
            else
            {
                FwSqlSelect.PagingCompatibility = FwSqlSelect.PagingCompatibilities.Sql2012;
                Console.WriteLine("SqlSelect Paging Compatibility: Sql2012");
            }
            AlertFunc.RefreshAlerts(ApplicationConfig);
        }
        //------------------------------------------------------------------------------------
        protected abstract void AddSwaggerDocs(SwaggerGenOptions options);
        //------------------------------------------------------------------------------------
        protected virtual void ConfigureStaticFileHosting(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var wwwrootFileServerOptions = new FileServerOptions();
            wwwrootFileServerOptions.StaticFileOptions.OnPrepareResponse = context =>
            {
                // Only cache the images in wwwroot
                bool cacheResults =
                    context.File.Name.ToLower().EndsWith(".png") ||
                    context.File.Name.ToLower().EndsWith(".jpg") ||
                    context.File.Name.ToLower().EndsWith(".jpeg") ||
                    context.File.Name.ToLower().EndsWith(".gif");
                if (!cacheResults)
                {
                    context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                    context.Context.Response.Headers.Add("Expires", "-1");
                }
            };
            app.UseFileServer(wwwrootFileServerOptions);
        }
        //------------------------------------------------------------------------------------
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            FwLogger.LoggerFactory = loggerFactory;
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //app.FwMaintainCorsHeaders();  // this can maybe be removed after .net core 2.2, which is supposed to fix CORS on 500 errors, currently the headers are getting dropped
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    await Task.CompletedTask;
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();   
                    if (exceptionHandlerPathFeature?.Error is ArgumentException)
                    {
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "application/json";
                        context.Response.Headers["Access-Control-Allow-Origin"] = "*";
                        var argumentException = (ArgumentException)exceptionHandlerPathFeature?.Error;
                        ModelStateDictionary modelState = new ModelStateDictionary();
                        modelState.AddModelError(argumentException.ParamName, argumentException.Message);
                        var serializableModelState = new SerializableError(modelState);
                        string argumentExceptionJson = JsonConvert.SerializeObject(serializableModelState);
                        await context.Response.WriteAsync(argumentExceptionJson);
                        return;
                    }
                    var apiException = new FwApiException();
                    if (exceptionHandlerPathFeature?.Error is Exception)
                    {
                        apiException.Message = exceptionHandlerPathFeature.Error.Message;
                        apiException.StackTrace = exceptionHandlerPathFeature.Error.StackTrace;
                    }
                    else
                    {
                        apiException.Message = "An unknown error has occured.";
                    }
                    apiException.StatusCode = 500;
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    context.Response.Headers["Access-Control-Allow-Origin"] = "*";
                    string json = JsonConvert.SerializeObject(apiException);
                    await context.Response.WriteAsync(json);
                    return;
                });
            });

            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors(builder =>
               builder
               .AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials()
               .SetPreflightMaxAge(TimeSpan.FromDays(7))); // this line keeps the browser from pre-flighting every request

            this.ConfigureStaticFileHosting(app, env, loggerFactory);

            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger(o =>
            {
                o.PreSerializeFilters.Add((document, request) =>
                {
                    // lowercase the urls displayed in swagger
                    document.Paths = document.Paths.ToDictionary(p => p.Key.ToLowerInvariant(), p => p.Value);
                });
            });
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = SystemName + " API";
                if (File.Exists(Path.Combine(HostingEnvironment.ContentRootPath, "swagger-ui/custom.css")))
                {
                    c.InjectStylesheet("/swagger-ui/custom.css", "all");
                }
                if (File.Exists(Path.Combine(HostingEnvironment.ContentRootPath, "swagger-ui/custom.js")))
                {
                    c.InjectJavascript("/swagger-ui/custom.js", "text/javascript");
                }
                c.DocExpansion(DocExpansion.None);
                this.AddSwaggerEndPoints(c);
            });

            var serverAddressesFeature = app.ServerFeatures.Get<IServerAddressesFeature>();
            foreach (var serverAddress in serverAddressesFeature.Addresses)
            {
                var ipAddresses = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
                foreach (var ipAddress in ipAddresses)
                {
                    if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                    {
                        string address = serverAddress.Replace("0.0.0.0", ipAddress.ToString());
                        Console.WriteLine($"Now listening on: {address}");
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------
        protected abstract void AddSwaggerEndPoints(SwaggerUIOptions options);
        //------------------------------------------------------------------------------------
    }
}
