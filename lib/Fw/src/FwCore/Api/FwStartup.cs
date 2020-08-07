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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Converters;
using OfficeOpenXml.ConditionalFormatting;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        protected string SystemName;
        //------------------------------------------------------------------------------------
        public FwStartup(IHostingEnvironment env, string systemName)
        {
            HostingEnvironment = env;
            SystemName = systemName;
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
            if (string.IsNullOrEmpty(ApplicationConfig.JwtIssuerOptions.SecretKey))
            {
                throw new Exception("ApplicationConfig.JwtIssuerOptions.SecretKey must be set in appsettings.json, here's a randomly generated key you can use: " + Guid.NewGuid().ToString().Replace("-", "").ToUpper() + Guid.NewGuid().ToString().Replace("-", "").ToUpper());
            }
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ApplicationConfig.JwtIssuerOptions.SecretKey));
            services.Configure<FwJwtIssuerOptions>(options =>
            {
                options.Authority = ApplicationConfig.JwtIssuerOptions.Authority;
                options.Issuer    = ApplicationConfig.JwtIssuerOptions.Issuer;
                options.Audience  = ApplicationConfig.JwtIssuerOptions.Audience;
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
            FwSqlSelect.PagingCompatibility = FwSqlSelect.PagingCompatibilities.Sql2012;
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
                context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                context.Context.Response.Headers.Add("Expires", "-1");
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

            //redirect to HTTPS in production configuration
            //if (HostingEnvironment.IsProduction())
            //{
            //    var options = new RewriteOptions()
            //        .AddRedirectToHttps();
            //    app.UseRewriter(options);
            //}

            // shows an exception page
            //if (env.IsDevelopment())
            app.FwMaintainCorsHeaders();  // this can maybe be removed after .net core 2.2, which is supposed to fix CORS on 500 errors, currently the headers are getting dropped
            app.UseDeveloperExceptionPage();

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
            if (!env.IsDevelopment())
            {
                app.Run(context => {
                    context.Response.Redirect(Configuration["ApplicationConfig:VirtualDirectory"] + "/swagger");
                    return Task.CompletedTask;
                });
            }
        }
        //------------------------------------------------------------------------------------
        protected abstract void AddSwaggerEndPoints(SwaggerUIOptions options);
        //------------------------------------------------------------------------------------
    }
}
