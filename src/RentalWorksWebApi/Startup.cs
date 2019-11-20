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
            //FwSecurityTree.Tree = new SecurityTree(ApplicationConfig.DatabaseSettings, "{94FBE349-104E-420C-81E9-1636EBAE2836}");
            RwGlobals.SetGlobalColors(ApplicationConfig.DatabaseSettings);
            FwAppManager.Tree = new AppManager(this.ApplicationConfig.DatabaseSettings);
            FwAppManager.CurrentProduct = "Rw";
            FwAppManager.CurrentProductEdition = "E";
            FwAppManager.Tree.LoadFromWebApi();
            //FwAppManager.Tree.LoadAllGroupTrees().Wait();
            //var node1 = FwAppManager.Tree.GetGroupsTreeAsync("A000KXE5", false).Result;
            //var node2 = FwAppManager.Tree.GetGroupsTreeAsync("A000KXE5", false).Result;
        }
        //------------------------------------------------------------------------------------
        public override void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            base.Configure(app, env, loggerFactory);
        }
        //------------------------------------------------------------------------------------
    }
}
