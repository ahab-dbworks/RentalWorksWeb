using FwCore.Api;
using FwStandard.AppManager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApi.ApplicationManager;
using WebApi.Modules.HomeControls.InventoryAvailability;
using WebApi;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;

namespace WebApi
{
    public class Startup : FwStartup
    {
        //------------------------------------------------------------------------------------
        public Startup(IHostingEnvironment env) :  base(env, "RentalWorks")
        {
            
        }
        //------------------------------------------------------------------------------------
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            RwGlobals.SetGlobalColors(ApplicationConfig.DatabaseSettings);
            FwAppManager.Tree = new AppManager(this.ApplicationConfig.DatabaseSettings);
            FwAppManager.CurrentProduct = "Rw";
            FwAppManager.CurrentProductEdition = "E";
            FwAppManager.Tree.LoadFromWebApi();
            FwAppManager.Tree.LoadAllGroupTrees().Wait();
            if (this.ApplicationConfig.EnableAvailabilityService)
            {
                services.AddHostedService<AvailabilityService>();
            }
        }
        //------------------------------------------------------------------------------------
        public override void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            base.Configure(app, env, loggerFactory);
        }
        //------------------------------------------------------------------------------------
        protected override void AddSwaggerDocs(SwaggerGenOptions options)
        {
            options.SwaggerDoc("accountservices-v1", new Info { Title = SystemName + " Account Services API v1", Version = "v1" });
            options.SwaggerDoc("home-v1", new Info { Title = SystemName + " Home API v1", Version = "v1" });
            options.SwaggerDoc("settings-v1", new Info { Title = SystemName + " Settings API v1", Version = "v1" });
            options.SwaggerDoc("reports-v1", new Info { Title = SystemName + "  Reports API v1", Version = "v1" });
            options.SwaggerDoc("utilities-v1", new Info { Title = SystemName + "  Utilities API v1", Version = "v1" });
            options.SwaggerDoc("administrator-v1", new Info { Title = SystemName + " Administrator API v1", Version = "v1" });
            options.SwaggerDoc("mobile-v1", new Info { Title = SystemName + " QuikScan API v1", Version = "v1" });
        }
        //------------------------------------------------------------------------------------
        protected override void AddSwaggerEndPoints(SwaggerUIOptions options)
        {
            options.SwaggerEndpoint(Configuration["ApplicationConfig:VirtualDirectory"] + "/swagger/accountservices-v1/swagger.json", SystemName + " Account Services API v1");
            options.SwaggerEndpoint(Configuration["ApplicationConfig:VirtualDirectory"] + "/swagger/home-v1/swagger.json", SystemName + " Home API v1");
            options.SwaggerEndpoint(Configuration["ApplicationConfig:VirtualDirectory"] + "/swagger/settings-v1/swagger.json", SystemName + " Settings API v1");
            options.SwaggerEndpoint(Configuration["ApplicationConfig:VirtualDirectory"] + "/swagger/reports-v1/swagger.json", SystemName + " Reports API v1");
            options.SwaggerEndpoint(Configuration["ApplicationConfig:VirtualDirectory"] + "/swagger/utilities-v1/swagger.json", SystemName + " Utilities API v1");
            options.SwaggerEndpoint(Configuration["ApplicationConfig:VirtualDirectory"] + "/swagger/administrator-v1/swagger.json", SystemName + " Administrator API v1");
            options.SwaggerEndpoint(Configuration["ApplicationConfig:VirtualDirectory"] + "/swagger/mobile-v1/swagger.json", SystemName + " Mobile API v1");
        }
        //------------------------------------------------------------------------------------
        protected override void ConfigureStaticFileHosting(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            base.ConfigureStaticFileHosting(app, env, loggerFactory);

            if (env.IsDevelopment())
            {
                var mobileFileServerOptions = new FileServerOptions();
                mobileFileServerOptions.RequestPath = "/quikscandev";
                mobileFileServerOptions.EnableDefaultFiles = true;
                mobileFileServerOptions.FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "quikscan"));
                var mobilefileExtensionContentTypeProvider = new FileExtensionContentTypeProvider();
                mobilefileExtensionContentTypeProvider.Mappings[".json"] = "text/json";
                mobileFileServerOptions.StaticFileOptions.ContentTypeProvider = mobilefileExtensionContentTypeProvider;
                //quikscanFileServerOptions.StaticFileOptions.OnPrepareResponse = context =>
                //{
                //    context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                //    context.Context.Response.Headers.Add("Expires", "-1");
                //};
                app.UseFileServer(mobileFileServerOptions);

                //var webFileServerOptions = new FileServerOptions();
                //webFileServerOptions.RequestPath = "/dev/web";
                //webFileServerOptions.EnableDefaultFiles = true;
                //webFileServerOptions.FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Web"));
                //var webFileExtensionContentTypeProvider = new FileExtensionContentTypeProvider();
                //webFileExtensionContentTypeProvider.Mappings[".json"] = "text/json";
                //webFileServerOptions.StaticFileOptions.ContentTypeProvider = webFileExtensionContentTypeProvider;
                ////quikscanFileServerOptions.StaticFileOptions.OnPrepareResponse = context =>
                ////{
                ////    context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                ////    context.Context.Response.Headers.Add("Expires", "-1");
                ////};
                //app.UseFileServer(webFileServerOptions);
            }
        }
        //------------------------------------------------------------------------------------
    }
}
