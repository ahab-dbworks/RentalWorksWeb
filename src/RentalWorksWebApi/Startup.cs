using FwCore.Api;
using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Reflection;
using WebApi.ApplicationManager;
using WebApi.Middleware;
using WebApi.Modules.HomeControls.BillingSchedule;
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
            if (this.ApplicationConfig.EnableBillingScheduleService)
            {
                services.AddHostedService<BillingScheduleService>();
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
            bool needToUpgradeAppSettingsJson = false;
            if (this.ApplicationConfig.WebApp != null || this.ApplicationConfig.WebRequestPath != null || 
                this.ApplicationConfig.MobileApp != null || this.ApplicationConfig.MobileRequestPath != null)
            {
                needToUpgradeAppSettingsJson = true;
            }
            if (this.ApplicationConfig.WebApp != null)
            {
                var rentalworksApp = new FwStandard.Models.App();
                rentalworksApp.Path = this.ApplicationConfig.WebRequestPath;
                rentalworksApp.DevPath = "/rentalworksdev";
                rentalworksApp.ApplicationConfig = new System.Collections.Concurrent.ConcurrentDictionary<string, object>();
                this.ApplicationConfig.Apps["rentalworks"] = rentalworksApp;
                var webAppConfigType = typeof(WebAppConfig);
                var webAppConfigProperties = webAppConfigType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                foreach (var propertyInfo in webAppConfigProperties)
                {
                    object val = propertyInfo.GetValue(this.ApplicationConfig.WebApp);
                    if (val != null)
                    {
                        rentalworksApp.ApplicationConfig[propertyInfo.Name] = val;
                    }
                }
            }

            if (this.ApplicationConfig.MobileApp != null)
            {
                var quikscanApp = new FwStandard.Models.App();
                quikscanApp.Path = this.ApplicationConfig.MobileRequestPath;
                quikscanApp.DevPath = "/quikscandev";
                quikscanApp.ApplicationConfig = new System.Collections.Concurrent.ConcurrentDictionary<string, object>();
                this.ApplicationConfig.Apps["quikscan"] = quikscanApp;
                var mobileAppConfigType = typeof(MobileAppConfig);
                var mobileAppConfigProperties = mobileAppConfigType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                foreach (var propertyInfo in mobileAppConfigProperties)
                {
                    object val = propertyInfo.GetValue(this.ApplicationConfig.MobileApp);
                    if (val != null)
                    {
                        quikscanApp.ApplicationConfig[propertyInfo.Name] = val;
                    }
                }
            }

            // Host RentalWorks if Apps.rentalworks is defined in the appsettings.json
            if (this.ApplicationConfig.Apps.ContainsKey("rentalworks"))
            {
                if (this.ApplicationConfig.Apps["rentalworks"].DevPath == null)
                {
                    this.ApplicationConfig.Apps["rentalworks"].DevPath = "/rentalworksdev";
                }

                // host rentalworksweb prod
                if (this.ApplicationConfig.Apps.ContainsKey("rentalworks"))
                {
                    string rentalworksProdRequestPath = "";
                    var rentalworksProdDir = Path.Combine(Environment.CurrentDirectory, $"apps{Path.DirectorySeparatorChar}rentalworks");
                    if (Directory.Exists(rentalworksProdDir))
                    {
                        var rentalworksProdFileServerOptions = new FileServerOptions();
                        if (this.ApplicationConfig.Apps["rentalworks"].Path != null)
                        {
                            rentalworksProdRequestPath = this.ApplicationConfig.Apps["rentalworks"].Path;
                        }
                        rentalworksProdFileServerOptions.RequestPath = rentalworksProdRequestPath;
                        rentalworksProdFileServerOptions.EnableDefaultFiles = true;
                        rentalworksProdFileServerOptions.FileProvider = new PhysicalFileProvider(rentalworksProdDir);
                        var rentalworksProdFileExtensionContentTypeProvider = new FileExtensionContentTypeProvider();
                        rentalworksProdFileExtensionContentTypeProvider.Mappings[".json"] = "application/json";
                        rentalworksProdFileExtensionContentTypeProvider.Mappings[".min.css"] = "text/css";
                        rentalworksProdFileServerOptions.StaticFileOptions.ContentTypeProvider = rentalworksProdFileExtensionContentTypeProvider;
                        rentalworksProdFileServerOptions.StaticFileOptions.OnPrepareResponse = context =>
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
                        app.UseFileServer(rentalworksProdFileServerOptions);
                        Console.WriteLine("------------------------------------------------------------------------------------");
                        Console.WriteLine("Hosting RentalWorks at:");
                        Console.WriteLine($"  url: \"{rentalworksProdRequestPath}\"");
                        Console.WriteLine($"  path: \"{rentalworksProdDir}\"");
                    }
                }

                // host rentalworksweb dev
                if (env.IsDevelopment())
                {
                    string pathWebApiProject = Environment.CurrentDirectory;
                    string pathSrcFolder = Path.GetDirectoryName(pathWebApiProject);
                    string rentalworksDevRequestPath = this.ApplicationConfig.Apps["rentalworks"].DevPath;
                    var rentalworksDevDir = Path.Combine(pathSrcFolder, "RentalWorksWeb");
                    if (Directory.Exists(rentalworksDevDir))
                    {
                        var rentalworksDevFileServerOptions = new FileServerOptions();
                        rentalworksDevFileServerOptions.RequestPath = rentalworksDevRequestPath;
                        rentalworksDevFileServerOptions.EnableDefaultFiles = true;
                        rentalworksDevFileServerOptions.FileProvider = new PhysicalFileProvider(rentalworksDevDir);
                        var rentalworksDevFileExtensionContentTypeProvider = new FileExtensionContentTypeProvider();
                        rentalworksDevFileExtensionContentTypeProvider.Mappings[".json"] = "application/json";
                        rentalworksDevFileExtensionContentTypeProvider.Mappings[".min.css"] = "text/css";
                        rentalworksDevFileExtensionContentTypeProvider.Mappings[".ts"] = "application/typescript";
                        rentalworksDevFileExtensionContentTypeProvider.Mappings[".js.map"] = "application/json";
                        rentalworksDevFileServerOptions.StaticFileOptions.ContentTypeProvider = rentalworksDevFileExtensionContentTypeProvider;
                        rentalworksDevFileServerOptions.StaticFileOptions.OnPrepareResponse = context =>
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
                        app.UseFileServer(rentalworksDevFileServerOptions);
                        Console.WriteLine("------------------------------------------------------------------------------------");
                        Console.WriteLine("Hosting RentalWorksDev at:");
                        Console.WriteLine($"  url: \"{rentalworksDevRequestPath}\"");
                        Console.WriteLine($"  path: \"{rentalworksDevDir}\"");
                    }
                }
            }
            if (this.ApplicationConfig.Apps.ContainsKey("trakitworks"))
            {
                if (this.ApplicationConfig.Apps["trakitworks"].DevPath == null)
                {
                    this.ApplicationConfig.Apps["trakitworks"].DevPath = "/trakitworksdev";
                }

                // host trakitworks prod
                string trakitworksProdRequestPath = "/trakitworks";
                var trakitworksProdDir = Path.Combine(Environment.CurrentDirectory, $"apps{Path.DirectorySeparatorChar}trakitworks");
                if (Directory.Exists(trakitworksProdDir))
                {
                    var trakitworksProdFileServerOptions = new FileServerOptions();
                    if (this.ApplicationConfig.Apps["trakitworks"].Path != null)
                    {
                        trakitworksProdRequestPath = this.ApplicationConfig.Apps["trakitworks"].Path;
                    }
                    trakitworksProdFileServerOptions.RequestPath = trakitworksProdRequestPath;
                    trakitworksProdFileServerOptions.EnableDefaultFiles = true;
                    trakitworksProdFileServerOptions.FileProvider = new PhysicalFileProvider(trakitworksProdDir);
                    var trakitworksProdFileExtensionContentTypeProvider = new FileExtensionContentTypeProvider();
                    trakitworksProdFileExtensionContentTypeProvider.Mappings[".json"] = "application/json";
                    trakitworksProdFileExtensionContentTypeProvider.Mappings[".min.css"] = "text/css";
                    trakitworksProdFileServerOptions.StaticFileOptions.ContentTypeProvider = trakitworksProdFileExtensionContentTypeProvider;
                    trakitworksProdFileServerOptions.StaticFileOptions.OnPrepareResponse = context =>
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
                    app.UseFileServer(trakitworksProdFileServerOptions);
                    Console.WriteLine("------------------------------------------------------------------------------------");
                    Console.WriteLine("Hosting TrakItWorks at:");
                    Console.WriteLine($"  url: \"{trakitworksProdRequestPath}\"");
                    Console.WriteLine($"  path: \"{trakitworksProdDir}\"");
                }

                // host trakitworks dev
                if (env.IsDevelopment())
                {
                    string pathWebApiProject = Environment.CurrentDirectory;
                    string pathSrcFolder = Path.GetDirectoryName(pathWebApiProject);
                    string trakitworksDevRequestPath = this.ApplicationConfig.Apps["trakitworks"].DevPath;
                    var trakitworksDevDir = Path.Combine(pathSrcFolder, $"RentalWorksWebApi{Path.DirectorySeparatorChar}TrakItWorks");
                    if (Directory.Exists(trakitworksDevDir))
                    {
                        var trakitworksDevFileServerOptions = new FileServerOptions();
                        trakitworksDevFileServerOptions.RequestPath = trakitworksDevRequestPath;
                        trakitworksDevFileServerOptions.EnableDefaultFiles = true;
                        trakitworksDevFileServerOptions.FileProvider = new PhysicalFileProvider(trakitworksDevDir);
                        var trakitworksDevFileExtensionContentTypeProvider = new FileExtensionContentTypeProvider();
                        trakitworksDevFileExtensionContentTypeProvider.Mappings[".json"] = "application/json";
                        trakitworksDevFileExtensionContentTypeProvider.Mappings[".min.css"] = "text/css";
                        trakitworksDevFileExtensionContentTypeProvider.Mappings[".ts"] = "application/typescript";
                        trakitworksDevFileExtensionContentTypeProvider.Mappings[".js.map"] = "application/json";
                        trakitworksDevFileServerOptions.StaticFileOptions.ContentTypeProvider = trakitworksDevFileExtensionContentTypeProvider;
                        trakitworksDevFileServerOptions.StaticFileOptions.OnPrepareResponse = context =>
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
                        app.UseFileServer(trakitworksDevFileServerOptions);
                        Console.WriteLine("------------------------------------------------------------------------------------");
                        Console.WriteLine("Hosting TrakItWorksDev at:");
                        Console.WriteLine($"  url: \"{trakitworksDevRequestPath}\"");
                        Console.WriteLine($"  path: \"{trakitworksDevDir}\"");
                    }
                }
            }
            if (this.ApplicationConfig.Apps.ContainsKey("quikscan"))
            {
                if (this.ApplicationConfig.Apps["quikscan"].DevPath == null)
                {
                    this.ApplicationConfig.Apps["quikscan"].DevPath = "/quikscandev";
                }

                // host quikscan prod
                var quikscanProdDir = Path.Combine(Environment.CurrentDirectory, $"apps{Path.DirectorySeparatorChar}quikscan");
                if (Directory.Exists(quikscanProdDir))
                {
                    var quikscanProdFileServerOptions = new FileServerOptions();
                    string quikscanProdRequestPath = "/quikscan";
                    if (this.ApplicationConfig.Apps["quikscan"].Path != null)
                    {
                        quikscanProdRequestPath = this.ApplicationConfig.Apps["quikscan"].Path;
                    }
                    quikscanProdFileServerOptions.RequestPath = quikscanProdRequestPath;
                    quikscanProdFileServerOptions.EnableDefaultFiles = true;
                    quikscanProdFileServerOptions.FileProvider = new PhysicalFileProvider(quikscanProdDir);
                    var mobileProdfileExtensionContentTypeProvider = new FileExtensionContentTypeProvider();
                    mobileProdfileExtensionContentTypeProvider.Mappings[".json"] = "application/json";
                    mobileProdfileExtensionContentTypeProvider.Mappings[".css"] = "text/css";
                    quikscanProdFileServerOptions.StaticFileOptions.ContentTypeProvider = mobileProdfileExtensionContentTypeProvider;
                    quikscanProdFileServerOptions.StaticFileOptions.OnPrepareResponse = context =>
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
                    app.UseFileServer(quikscanProdFileServerOptions);
                    Console.WriteLine("------------------------------------------------------------------------------------");
                    Console.WriteLine("Hosting QuikScan at:");
                    Console.WriteLine($"  url: \"{quikscanProdRequestPath}\"");
                    Console.WriteLine($"  path: \"{quikscanProdDir}\"");
                }

                // host quikscan dev
                if (env.IsDevelopment())
                {
                    string quikscanDevPath = this.ApplicationConfig.Apps["quikscan"].DevPath;
                    var quikscanDevDir = Path.Combine(Environment.CurrentDirectory, "QuikScan");
                    if (Directory.Exists(quikscanDevDir))
                    {
                        var quikscanDevFileServerOptions = new FileServerOptions();
                        quikscanDevFileServerOptions.RequestPath = quikscanDevPath;
                        quikscanDevFileServerOptions.EnableDefaultFiles = true;
                        quikscanDevFileServerOptions.FileProvider = new PhysicalFileProvider(quikscanDevDir);
                        var mobileDevfileExtensionContentTypeProvider = new FileExtensionContentTypeProvider();
                        mobileDevfileExtensionContentTypeProvider.Mappings[".json"] = "application/json";
                        mobileDevfileExtensionContentTypeProvider.Mappings[".min.css"] = "text/css";
                        mobileDevfileExtensionContentTypeProvider.Mappings[".ts"] = "application/typescript";
                        mobileDevfileExtensionContentTypeProvider.Mappings[".js.map"] = "application/json";
                        quikscanDevFileServerOptions.StaticFileOptions.ContentTypeProvider = mobileDevfileExtensionContentTypeProvider;
                        quikscanDevFileServerOptions.StaticFileOptions.OnPrepareResponse = context =>
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
                        app.UseFileServer(quikscanDevFileServerOptions);
                        Console.WriteLine("------------------------------------------------------------------------------------");
                        Console.WriteLine("Hosting QuikScanDev at:");
                        Console.WriteLine($"  url: \"{quikscanDevPath}\"");
                        Console.WriteLine($"  path: \"{quikscanDevDir}\"");
                    }
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
                wwwrootFileServerOptions.FileProvider = new PhysicalFileProvider(wwwrootDir); ;
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

            if (needToUpgradeAppSettingsJson)
            {
                throw new Exception("The 'WebApp', 'WebRequestPath', 'MobileApp', and 'MobileRequestPath' sections in appsettings.json have been replaced with the new 'Apps' section.  See appsettings.sample.json for an example.");
                //var appsJson = "\"Apps\":" + JsonConvert.SerializeObject(this.ApplicationConfig.Apps);
                //Console.BackgroundColor = ConsoleColor.Red;
                //Console.ForegroundColor = ConsoleColor.White;
                //Console.Error.WriteLine(appsJson);
                //Console.ResetColor();
                //Console.WriteLine("------------------------------------------------------------------------------------");
            }
        }
        //------------------------------------------------------------------------------------
    }
}
