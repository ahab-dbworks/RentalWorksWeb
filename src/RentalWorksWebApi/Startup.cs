using FwCore.Api;
using FwStandard.Models;
using FwStandard.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApi.Modules.Home.InventoryAvailability;
using WebLibrary;
using WebLibrary.Security;

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
            FwSecurityTree.Tree = new SecurityTree(ApplicationConfig.DatabaseSettings, "{94FBE349-104E-420C-81E9-1636EBAE2836}");
            RwGlobals.SetGlobalColors(ApplicationConfig.DatabaseSettings);
        }
        //------------------------------------------------------------------------------------
        public override void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            base.Configure(app, env, loggerFactory);
        }
        //------------------------------------------------------------------------------------
    }
}
