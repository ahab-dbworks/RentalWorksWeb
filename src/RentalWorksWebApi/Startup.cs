using AutoMapper;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using RentalWorksWebApi.Options;
using RentalWorksWebDataLayer.Settings;
using RentalWorksWebLogic.Settings;
using System;
using System.Text;

namespace RentalWorksWebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<FwSqlMapperProfile>();
                cfg.CreateMap<CustomerStatusLogic, CustomerStatusRecord>();
                cfg.CreateMap<GlAccountLogic, GlAccountRecord>();
                cfg.CreateMap<OrderLogic, OrderLoader>();
                cfg.CreateMap<OrderLoader, OrderLogic>();
                cfg.CreateMap<VendorClassLogic, VendorClassRecord>();
                cfg.CreateMap<CustomerTypeLogic, CustomerTypeRecord>();
                cfg.CreateMap<CreditStatusLogic, CreditStatusRecord>();
                cfg.CreateMap<WarehouseLogic, WarehouseRecord>();
                cfg.CreateMap<BillingCycleLoader, BillingCycleLogic>();
                cfg.CreateMap<BillingCycleLogic, BillingCycleLoader>();
                cfg.CreateMap<PaymentTypeLogic, PaymentTypeRecord>();
                cfg.CreateMap<CustomerCategoryLogic, CustomerCategoryRecord>();
                cfg.CreateMap<DealTypeLogic, DealTypeRecord>();
                cfg.CreateMap<PaymentTermsLogic, PaymentTermsRecord>();
            });
        }

        public IConfigurationRoot Configuration { get; }

        private const string SecretKey = "needtogetthisfromenvironment";
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // uncomment to configure IIS integration
            //services.Configure<IISOptions>(options => {
            //    options.
            //});

            services.AddCors();

            // Get ApplicationConfig section from appsettings.json
            // Adds services required for using options.
            // Register the IConfiguration instance which MyOptions binds against.
            ApplicationConfig appConfig = Configuration.GetSection("ApplicationConfig").Get<ApplicationConfig>();
            services.AddOptions();
            services.Configure<ApplicationConfig>(Configuration.GetSection("ApplicationConfig"));

            // Add framework services.

            // Make authentication compulsory across the board (i.e. shut
            // down EVERYTHING unless explicitly opened up).
            services
                .AddMvc(//config =>
                //{
                    //var policy = new AuthorizationPolicyBuilder()
                    //                    .RequireAuthenticatedUser()
                    //                    .Build();
                    //config.Filters.Add(new AuthorizeFilter(policy));
                //}
                )
                // don't use the lowerCamelCase formatter
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                })
            ;

            // Use policy auth.
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("User", policy => policy.RequireClaim("DisneyCharacter", "IAmMickey"));
            //});

            // Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            //services.Configure<JwtIssuerOptions>(options =>
            //{
            //    options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
            //    options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
            //    options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            ApplicationLogging.LoggerFactory = loggerFactory;

            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors(builder =>
               builder
               .AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials());

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

            app.UseMvc();
        }
    }
}
