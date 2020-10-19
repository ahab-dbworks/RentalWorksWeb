using FwCore.Api;
using FwStandard.AppManager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using WebApi.ApplicationManager;
using WebApi.Middleware;
using WebApi.Modules.HomeControls.InventoryAvailability;

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
            app.ApplicationConfigMiddleware();
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
            // host rentalworksweb prod
            if (this.ApplicationConfig.Apps.ContainsKey("rentalworks"))
            {
                string webProdRequestPath = "";
                var webProdDir = Path.Combine(Environment.CurrentDirectory, $"apps{Path.DirectorySeparatorChar}rentalworks");
                if (Directory.Exists(webProdDir))
                {
                    var webProdFileServerOptions = new FileServerOptions();
                    if (this.ApplicationConfig.Apps["rentalworks"].Path != null)
                    {
                        webProdRequestPath = this.ApplicationConfig.Apps["rentalworks"].Path;
                    }
                    webProdFileServerOptions.RequestPath = webProdRequestPath;
                    webProdFileServerOptions.EnableDefaultFiles = true;
                    webProdFileServerOptions.FileProvider = new PhysicalFileProvider(webProdDir);
                    var webProdFileExtensionContentTypeProvider = new FileExtensionContentTypeProvider();
                    webProdFileExtensionContentTypeProvider.Mappings[".json"] = "application/json";
                    webProdFileExtensionContentTypeProvider.Mappings[".min.css"] = "text/css";
                    webProdFileServerOptions.StaticFileOptions.ContentTypeProvider = webProdFileExtensionContentTypeProvider;
                    webProdFileServerOptions.StaticFileOptions.OnPrepareResponse = context =>
                    {
                        // host static files in quikscan folder, but only cache the images
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
                    app.UseFileServer(webProdFileServerOptions);
                    Console.WriteLine("------------------------------------------------------------------------------------");
                    Console.WriteLine("Hosting RentalWorks at:");
                    Console.WriteLine($"  url: \"{webProdRequestPath}\"");
                    Console.WriteLine($"  path: \"{webProdDir}\"");
                }
            }

            // host rentalworksweb dev
            if (env.IsDevelopment())
            {
                string pathWebApiProject = Environment.CurrentDirectory;
                string pathSrcFolder = Path.GetDirectoryName(pathWebApiProject);
                string webDevRequestPath = "/webdev";
                var webDevDir = Path.Combine(pathSrcFolder, "RentalWorksWeb");
                if (Directory.Exists(webDevDir))
                {
                    var webDevFileServerOptions = new FileServerOptions();
                    webDevFileServerOptions.RequestPath = webDevRequestPath;
                    webDevFileServerOptions.EnableDefaultFiles = true;
                    webDevFileServerOptions.FileProvider = new PhysicalFileProvider(webDevDir);
                    var webDevFileExtensionContentTypeProvider = new FileExtensionContentTypeProvider();
                    webDevFileExtensionContentTypeProvider.Mappings[".json"] = "application/json";
                    webDevFileExtensionContentTypeProvider.Mappings[".min.css"] = "text/css";
                    webDevFileExtensionContentTypeProvider.Mappings[".ts"] = "application/typescript";
                    webDevFileExtensionContentTypeProvider.Mappings[".js.map"] = "application/json";
                    webDevFileServerOptions.StaticFileOptions.ContentTypeProvider = webDevFileExtensionContentTypeProvider;
                    webDevFileServerOptions.StaticFileOptions.OnPrepareResponse = context =>
                    {
                        // host static files in quikscan folder, but only cache the images
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
                    app.UseFileServer(webDevFileServerOptions);
                    Console.WriteLine("------------------------------------------------------------------------------------");
                    Console.WriteLine("Hosting RentalWorksDev at:");
                    Console.WriteLine($"  url: \"{webDevRequestPath}\"");
                    Console.WriteLine($"  path: \"{webDevDir}\"");
                }
            }

            // host trakitworks prod
            if (this.ApplicationConfig.Apps.ContainsKey("trakitworks"))
            {
                string trakitworksProdRequestPath = "/trakitworks";
                var trakitworksProdDir = Path.Combine(Environment.CurrentDirectory, $"apps{Path.DirectorySeparatorChar}trakitworks");
                if (Directory.Exists(trakitworksProdDir))
                {
                    var webProdFileServerOptions = new FileServerOptions();
                    if (this.ApplicationConfig.Apps["trakitworks"].Path != null)
                    {
                        trakitworksProdRequestPath = this.ApplicationConfig.Apps["trakitworks"].Path;
                    }
                    webProdFileServerOptions.RequestPath = trakitworksProdRequestPath;
                    webProdFileServerOptions.EnableDefaultFiles = true;
                    webProdFileServerOptions.FileProvider = new PhysicalFileProvider(trakitworksProdDir);
                    var webProdFileExtensionContentTypeProvider = new FileExtensionContentTypeProvider();
                    webProdFileExtensionContentTypeProvider.Mappings[".json"] = "application/json";
                    webProdFileExtensionContentTypeProvider.Mappings[".min.css"] = "text/css";
                    webProdFileServerOptions.StaticFileOptions.ContentTypeProvider = webProdFileExtensionContentTypeProvider;
                    webProdFileServerOptions.StaticFileOptions.OnPrepareResponse = context =>
                    {
                        // host static files in quikscan folder, but only cache the images
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
                    app.UseFileServer(webProdFileServerOptions);
                    Console.WriteLine("------------------------------------------------------------------------------------");
                    Console.WriteLine("Hosting TrakItWorks at:");
                    Console.WriteLine($"  url: \"{trakitworksProdRequestPath}\"");
                    Console.WriteLine($"  path: \"{trakitworksProdDir}\"");
                }
            }

            // host trakitworks dev
            if (env.IsDevelopment())
            {
                string pathWebApiProject = Environment.CurrentDirectory;
                string pathSrcFolder = Path.GetDirectoryName(pathWebApiProject);
                string trakitworksDevRequestPath = "/trakitworksdev";
                var trakitworksDevDir = Path.Combine(pathSrcFolder, $"RentalWorksWebApi{Path.DirectorySeparatorChar}TrakItWorks");
                if (Directory.Exists(trakitworksDevDir))
                {
                    var webDevFileServerOptions = new FileServerOptions();
                    webDevFileServerOptions.RequestPath = trakitworksDevRequestPath;
                    webDevFileServerOptions.EnableDefaultFiles = true;
                    webDevFileServerOptions.FileProvider = new PhysicalFileProvider(trakitworksDevDir);
                    var webDevFileExtensionContentTypeProvider = new FileExtensionContentTypeProvider();
                    webDevFileExtensionContentTypeProvider.Mappings[".json"] = "application/json";
                    webDevFileExtensionContentTypeProvider.Mappings[".min.css"] = "text/css";
                    webDevFileExtensionContentTypeProvider.Mappings[".ts"] = "application/typescript";
                    webDevFileExtensionContentTypeProvider.Mappings[".js.map"] = "application/json";
                    webDevFileServerOptions.StaticFileOptions.ContentTypeProvider = webDevFileExtensionContentTypeProvider;
                    webDevFileServerOptions.StaticFileOptions.OnPrepareResponse = context =>
                    {
                        // host static files in quikscan folder, but only cache the images
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
                    app.UseFileServer(webDevFileServerOptions);
                    Console.WriteLine("------------------------------------------------------------------------------------");
                    Console.WriteLine("Hosting TrakItWorksDev at:");
                    Console.WriteLine($"  url: \"{trakitworksDevRequestPath}\"");
                    Console.WriteLine($"  path: \"{trakitworksDevDir}\"");
                }
            }

            // host quikscan prod
            if (this.ApplicationConfig.Apps.ContainsKey("quikscan"))
            {
                var mobileProdDir = Path.Combine(Environment.CurrentDirectory, $"apps{Path.DirectorySeparatorChar}quikscan");
                if (Directory.Exists(mobileProdDir))
                {
                    var mobileProdFileServerOptions = new FileServerOptions();
                    string mobileProdRequestPath = "/quikscan";
                    if (this.ApplicationConfig.Apps["rentalworks"].Path != null)
                    {
                        mobileProdRequestPath = this.ApplicationConfig.Apps["rentalworks"].Path;
                    }
                    mobileProdFileServerOptions.RequestPath = mobileProdRequestPath;
                    mobileProdFileServerOptions.EnableDefaultFiles = true;
                    mobileProdFileServerOptions.FileProvider = new PhysicalFileProvider(mobileProdDir);
                    var mobileProdfileExtensionContentTypeProvider = new FileExtensionContentTypeProvider();
                    mobileProdfileExtensionContentTypeProvider.Mappings[".json"] = "application/json";
                    mobileProdfileExtensionContentTypeProvider.Mappings[".css"] = "text/css";
                    mobileProdFileServerOptions.StaticFileOptions.ContentTypeProvider = mobileProdfileExtensionContentTypeProvider;
                    mobileProdFileServerOptions.StaticFileOptions.OnPrepareResponse = context =>
                    {
                        // host static files in quikscan folder, but only cache the images
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
                    app.UseFileServer(mobileProdFileServerOptions);
                    Console.WriteLine("------------------------------------------------------------------------------------");
                    Console.WriteLine("Hosting QuikScan at:");
                    Console.WriteLine($"  url: \"{mobileProdRequestPath}\"");
                    Console.WriteLine($"  path: \"{mobileProdDir}\"");
                }
            }

            // host quikscan dev
            if (env.IsDevelopment())
            {
                string mobileDevRequestPath = "/quikscandev";
                var mobileDevDir = Path.Combine(Environment.CurrentDirectory, "QuikScan");
                if (Directory.Exists(mobileDevDir))
                {
                    var mobileDevFileServerOptions = new FileServerOptions();
                    mobileDevFileServerOptions.RequestPath = mobileDevRequestPath;
                    mobileDevFileServerOptions.EnableDefaultFiles = true;
                    mobileDevFileServerOptions.FileProvider = new PhysicalFileProvider(mobileDevDir);
                    var mobileDevfileExtensionContentTypeProvider = new FileExtensionContentTypeProvider();
                    mobileDevfileExtensionContentTypeProvider.Mappings[".json"] = "application/json";
                    mobileDevfileExtensionContentTypeProvider.Mappings[".min.css"] = "text/css";
                    mobileDevfileExtensionContentTypeProvider.Mappings[".ts"] = "application/typescript";
                    mobileDevfileExtensionContentTypeProvider.Mappings[".js.map"] = "application/json";
                    mobileDevFileServerOptions.StaticFileOptions.ContentTypeProvider = mobileDevfileExtensionContentTypeProvider;
                    mobileDevFileServerOptions.StaticFileOptions.OnPrepareResponse = context =>
                    {
                        // host static files in quikscan folder, but only cache the images
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
                    app.UseFileServer(mobileDevFileServerOptions);
                    Console.WriteLine("------------------------------------------------------------------------------------");
                    Console.WriteLine("Hosting QuikScanDev at:");
                    Console.WriteLine($"  url: \"{mobileDevRequestPath}\"");
                    Console.WriteLine($"  path: \"{mobileDevDir}\"");
                }
            }

            // host static files in wwwroot
            var wwwrootDir = Path.Combine(Environment.CurrentDirectory, "wwwroot");
            var wwwrootRequestPath = "";
            if (Directory.Exists(wwwrootDir))
            {
                var wwwrootFileServerOptions = new FileServerOptions();
                wwwrootFileServerOptions.RequestPath = wwwrootRequestPath;
                wwwrootFileServerOptions.EnableDefaultFiles = true;
                wwwrootFileServerOptions.FileProvider = new PhysicalFileProvider(wwwrootDir);;
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
            Console.WriteLine("------------------------------------------------------------------------------------");
            Console.WriteLine("Hosting wwwroot at:");
            Console.WriteLine($"  url: \"{wwwrootRequestPath}\"");
            Console.WriteLine($"  path: \"{wwwrootDir}\"");
            Console.WriteLine("------------------------------------------------------------------------------------");
        }
        //------------------------------------------------------------------------------------
    }
}
