using FwStandard.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Modules.Home.InventoryAvailability
{
    internal class AvailabilityService : IHostedService, IDisposable
    {
        private Timer keepFreshTimer;
        private FwApplicationConfig appConfig;
        private int delayStartSeconds = 2;
        private int keepFreshSeconds = 15;
        //-------------------------------------------------------------------------------------------------------
        public AvailabilityService(IOptions<FwApplicationConfig> appConfig)
        {
            this.appConfig = appConfig.Value;
        }
        //-------------------------------------------------------------------------------------------------------
        public Task StartAsync(CancellationToken cancellationToken)
        {
            keepFreshTimer = new Timer(KeepFresh, null, TimeSpan.FromSeconds(delayStartSeconds), TimeSpan.FromSeconds(keepFreshSeconds));
            bool b = InventoryAvailabilityFunc.InitializeService(appConfig).Result;
            return Task.CompletedTask;
        }
        //-------------------------------------------------------------------------------------------------------
        private void DisableKeepFreshTimer()
        {
            keepFreshTimer?.Change(Timeout.Infinite, 0);  
        }
        //-------------------------------------------------------------------------------------------------------
        private void EnableKeepFreshTimer()
        {
            keepFreshTimer?.Change(TimeSpan.FromSeconds(keepFreshSeconds), TimeSpan.FromSeconds(keepFreshSeconds)); 
        }
        //-------------------------------------------------------------------------------------------------------
        private void KeepFresh(object state)
        {
            DisableKeepFreshTimer();
            bool b = InventoryAvailabilityFunc.KeepFresh(appConfig).Result;
            EnableKeepFreshTimer();
        }
        //-------------------------------------------------------------------------------------------------------
        public Task StopAsync(CancellationToken cancellationToken)
        {
            DisableKeepFreshTimer();
            return Task.CompletedTask;
        }
        //-------------------------------------------------------------------------------------------------------
        public void Dispose()
        {
            keepFreshTimer?.Dispose();
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
