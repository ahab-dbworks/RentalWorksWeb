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
using System;
using System.Text;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;

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

        private const string SecretKey = "needtogetthisfromenvironment";
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ApplicationConfig appConfig = Configuration.GetSection("ApplicationConfig").Get<ApplicationConfig>();
            services
                .AddCors()
                .AddOptions()
                .Configure<ApplicationConfig>(Configuration.GetSection("ApplicationConfig"))
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

            //require HTTPS in production configuration
            //if (HostingEnvironment.IsProduction())
            //{
            //    services.Configure<MvcOptions>(options =>
            //    {
            //        options.Filters.Add(new RequireHttpsAttribute());
            //    });
            //}

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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "RentalWorksWeb API", Version = "v1" });
                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "RentalWorksWebApi.xml");
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

            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors(builder =>
               builder
               .AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials());

            //app.UseDefaultFiles(); // Call first before app.UseStaticFiles()
            app.UseStaticFiles(); // For the wwwroot folder

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
