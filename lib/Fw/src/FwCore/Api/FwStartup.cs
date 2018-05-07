using AutoMapper;
using FwCore.Security;
using FwStandard.Models;
using FwStandard.Security;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FwCore.Api
{
    public class FwStartup
    {
        //------------------------------------------------------------------------------------
        public static IHostingEnvironment HostingEnvironment { get; private set; }
        public static IConfigurationRoot Configuration { get; private set; }
        public static FwApplicationConfig ApplicationConfig { get; private set; }
        public string SystemName { get; private set; }
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

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMissingTypeMaps = true;
            });
        }
        //------------------------------------------------------------------------------------
        public virtual void ConfigureServices(IServiceCollection services)
        {
            ApplicationConfig = Configuration.GetSection("ApplicationConfig").Get<FwApplicationConfig>();

            services.AddCors();

            services
                .AddOptions()
                .Configure<FwApplicationConfig>(Configuration.GetSection("ApplicationConfig"))
                .AddMvc(config =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    config.Filters.Add(new AuthorizeFilter(policy));
                })
                // don't use the lowerCamelCase formatter
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
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
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();

                // add an authorization policy for every controller method in the WebApi security tree
                FwSecurityTreeNode nodeCurrentApplication = FwSecurityTree.Tree.FindById(FwSecurityTree.CurrentApplicationId);
                // loop over each api top level menu
                foreach (var lv1menu in nodeCurrentApplication.Children)
                {
                    // loop over each controller
                    foreach (var controller in lv1menu.Children)
                    {
                        // loop over each controller method
                        foreach (var method in controller.Children)
                        {
                            // Adds a callback that will fire with each request on the controller method to validate whether the user has permission
                            options.AddPolicy("{" + method.Id + "}", policy => policy.AddRequirements(new SecurityTreeAuthorizationRequirement(method.Id, true, false)));
                        }
                    }
                }
            });
            services.AddSingleton<IAuthorizationHandler, SecurityTreeAuthorizationHandler>();

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
                        ValidateLifetime = false,

                        //ClockSkew = TimeSpan.Zero
                    };
                    
                })
            ;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("accountservices-v1", new Info { Title = SystemName + " Account Services API v1", Version = "v1" });
                c.SwaggerDoc("home-v1", new Info { Title = SystemName + " Home API v1", Version = "v1" });
                c.SwaggerDoc("settings-v1", new Info { Title = SystemName + " Settings API v1", Version = "v1" });
                c.SwaggerDoc("reports-v1", new Info { Title = SystemName + "  Reports API v1", Version = "v1" });
                c.SwaggerDoc("administrator-v1", new Info { Title = SystemName + " Administrator API v1", Version = "v1" });
                var filePath = Path.Combine(ApplicationEnvironment.ApplicationBasePath, "WebApi.xml");
                c.IncludeXmlComments(filePath);
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
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
            FwSqlSelect.PagingCompatibility = FwSqlSelect.PagingCompatibilities.PreSql2012;
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
            app.UseDeveloperExceptionPage();

            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors(builder =>
               builder
               .AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials()
               .SetPreflightMaxAge(TimeSpan.FromDays(7))); // this line keeps the browser from pre-flighting every request

            //app.UseDefaultFiles(); // Call first before app.UseStaticFiles()
            app.UseStaticFiles(); // For the wwwroot folder

            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Configuration["ApplicationConfig:VirtualDirectory"] + "/swagger/accountservices-v1/swagger.json", SystemName + " Account Services API v1");
                c.SwaggerEndpoint(Configuration["ApplicationConfig:VirtualDirectory"] + "/swagger/home-v1/swagger.json", SystemName + " Home API v1");
                c.SwaggerEndpoint(Configuration["ApplicationConfig:VirtualDirectory"] + "/swagger/settings-v1/swagger.json", SystemName + " Settings API v1");
                c.SwaggerEndpoint(Configuration["ApplicationConfig:VirtualDirectory"] + "/swagger/reports-v1/swagger.json", SystemName + " Reports API v1");
                c.SwaggerEndpoint(Configuration["ApplicationConfig:VirtualDirectory"] + "/swagger/administrator-v1/swagger.json", SystemName + " Administrator API v1");
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
    }
}
