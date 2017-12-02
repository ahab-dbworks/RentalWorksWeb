using AutoMapper;
using FwStandard.Models;
using FwStandard.Security;
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
using RentalWorksWebApi.Options;
using RentalWorksWebApi.Policies;
using RentalWorksWebLibrary;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RentalWorksWebApi
{
    public class Startup
    {
        public static IHostingEnvironment HostingEnvironment { get; private set; }
        public Startup(IHostingEnvironment env)
        {
            HostingEnvironment = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMissingTypeMaps = true;
            });
        }

        public IConfigurationRoot Configuration { get; }

        private const string SecretKey = "needtogetthisfromenvironment"; //
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ApplicationConfig appConfig = Configuration.GetSection("ApplicationConfig").Get<ApplicationConfig>();

            SqlServerConfig sqlServerConfig = new SqlServerConfig();
            sqlServerConfig.ConnectionString = appConfig.DatabaseSettings.ConnectionString;
            sqlServerConfig.QueryTimeout = appConfig.DatabaseSettings.QueryTimeout;
            sqlServerConfig.ReportTimeout = appConfig.DatabaseSettings.ReportTimeout;
            FwSecurityTree.Tree = new SecurityTree(sqlServerConfig, "{94FBE349-104E-420C-81E9-1636EBAE2836}");

            services
                .AddCors()
                .AddOptions()
                .Configure<ApplicationConfig>(Configuration.GetSection("ApplicationConfig"))
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

                // add an authorization policy for every controller method in the RentalWorksWebApi security tree
                FwSecurityTreeNode nodeRentalWorksWebApi = FwSecurityTree.Tree.FindById(FwSecurityTree.CurrentApplicationId);
                // loop over each api top level menu
                foreach (var lv1menu in nodeRentalWorksWebApi.Children)
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

            // Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions for dependency injection
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Authority = jwtAppSettingOptions[nameof(JwtIssuerOptions.Authority)];
                options.Issuer    = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience  = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            // this is the new ASP.NET Core 2.0 stuff for JWT (not sure about any other JWT stuff in this file anymore)
            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    //options.Configuration = new OpenIdConnectConfiguration();
                    //options.Configuration.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                    //options.Configuration.SigningKeys.Add(_signingKey);
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                        ValidateAudience = true,
                        ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = _signingKey,

                        RequireExpirationTime = false,
                        ValidateLifetime = false,

                        //ClockSkew = TimeSpan.Zero
                    };
                    //options.Authority            = jwtAppSettingOptions[nameof(JwtIssuerOptions.Authority)];
                    //options.Audience             = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                    //options.RequireHttpsMetadata = jwtAppSettingOptions["RequireHttpsMetadata"].ToLower().Equals("true"); //false;
                    
                })
            ;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "RentalWorksWeb API", Version = "v1" });
                var filePath = Path.Combine(ApplicationEnvironment.ApplicationBasePath, "RentalWorksWebApi.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //redirect to HTTPS in production configuration
            //if (HostingEnvironment.IsProduction())
            //{
            //    var options = new RewriteOptions()
            //        .AddRedirectToHttps();
            //    app.UseRewriter(options);
            //}

            ApplicationLogging.LoggerFactory = loggerFactory;

            // shows an exception page in dev mode
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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


            // this can be removed when jwt authentication is completed, this is how it was configured and working in core 1.1, but this no longer works in 2.0
            // Use Jwt to authenticate requests
            //var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            //app.UseJwtBearerAuthentication(new JwtBearerOptions
            //{
            //    AutomaticAuthenticate = true, //
            //    RequireHttpsMetadata = false, //don't require https
            //    TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

            //        ValidateAudience = true,
            //        ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = _signingKey,

            //        RequireExpirationTime = true,
            //        ValidateLifetime = true,

            //        ClockSkew = TimeSpan.Zero
            //    }
            //});
            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Configuration["ApplicationConfig:VirtualDirectory"] + "/swagger/v1/swagger.json", "RentalWorksWeb API v1");
            });
            app.Run(context => {
                context.Response.Redirect(Configuration["ApplicationConfig:VirtualDirectory"] + "/swagger");
                return Task.CompletedTask;
            });
        }
    }
}
