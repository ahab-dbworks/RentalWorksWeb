using FwCore.Api;
using FwStandard.AppManager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApi.ApplicationManager;
using WebApi.Modules.HomeControls.InventoryAvailability;
using WebApi;

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
            services.AddHostedService<AvailabilityService>();
            base.ConfigureServices(services);
            RwGlobals.SetGlobalColors(ApplicationConfig.DatabaseSettings);
            FwAppManager.Tree = new AppManager(this.ApplicationConfig.DatabaseSettings);
            FwAppManager.CurrentProduct = "Rw";
            FwAppManager.CurrentProductEdition = "E";
            FwAppManager.Tree.LoadFromWebApi();
        }
        //------------------------------------------------------------------------------------
        public override void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            base.Configure(app, env, loggerFactory);
        }
        //------------------------------------------------------------------------------------
    }
}
