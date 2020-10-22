using FwStandard.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Modules.Administrator.PlugIn
{
    internal class PlugInService: IHostedService, IDisposable
    {
        private FwApplicationConfig appConfig;

        //-------------------------------------------------------------------------------------------------------
        public PlugInService(IOptions<FwApplicationConfig> appConfig)
        {
            this.appConfig = appConfig.Value;
        }
        //-------------------------------------------------------------------------------------------------------
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        //-------------------------------------------------------------------------------------------------------
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        //-------------------------------------------------------------------------------------------------------
        public void Dispose()
        {
            //dispose something
        }
    }
}
