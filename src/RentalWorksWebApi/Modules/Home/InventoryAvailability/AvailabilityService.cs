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
        private Timer checkNeedRecalcTimer;
        private Timer keepFreshTimer;
        private FwApplicationConfig appConfig;
        private int delayCheckNeedRecalcStartSeconds = 2;
        private int delayKeepRefreshStartSeconds = 5;
        private int keepFreshSeconds = 15;
        private int checkNeedRecalcSeconds = 3;
        //-------------------------------------------------------------------------------------------------------
        public AvailabilityService(IOptions<FwApplicationConfig> appConfig)
        {
            this.appConfig = appConfig.Value;
        }
        //-------------------------------------------------------------------------------------------------------
        public Task StartAsync(CancellationToken cancellationToken)
        {
            checkNeedRecalcTimer = new Timer(CheckNeedRecalc, null, TimeSpan.FromSeconds(delayCheckNeedRecalcStartSeconds), TimeSpan.FromSeconds(checkNeedRecalcSeconds));
            keepFreshTimer = new Timer(KeepFresh, null, TimeSpan.FromSeconds(delayKeepRefreshStartSeconds), TimeSpan.FromSeconds(keepFreshSeconds));
            bool b = InventoryAvailabilityFunc.InitializeService(appConfig).Result;
            return Task.CompletedTask;
        }
        //-------------------------------------------------------------------------------------------------------
        private void DisableKeepFreshTimer()
        {
            keepFreshTimer?.Change(Timeout.Infinite, 0);  
        }
        //-------------------------------------------------------------------------------------------------------
        private void DisableCheckNeedRecalcTimer()
        {
            checkNeedRecalcTimer?.Change(Timeout.Infinite, 0);
        }
        //-------------------------------------------------------------------------------------------------------
        private void EnableKeepFreshTimer()
        {
            keepFreshTimer?.Change(TimeSpan.FromSeconds(keepFreshSeconds), TimeSpan.FromSeconds(keepFreshSeconds)); 
        }
        //-------------------------------------------------------------------------------------------------------
        private void EnableCheckNeedRecalcTimer()
        {
            checkNeedRecalcTimer?.Change(TimeSpan.FromSeconds(checkNeedRecalcSeconds), TimeSpan.FromSeconds(checkNeedRecalcSeconds));
        }
        //-------------------------------------------------------------------------------------------------------
        private void CheckNeedRecalc(object state)
        {
            DisableCheckNeedRecalcTimer();
            bool b = InventoryAvailabilityFunc.CheckNeedRecalc(appConfig).Result;
            EnableCheckNeedRecalcTimer();
        }
        //-------------------------------------------------------------------------------------------------------
        private void KeepFresh(object state)
        {
            DisableCheckNeedRecalcTimer();
            DisableKeepFreshTimer();
            bool b = InventoryAvailabilityFunc.KeepFresh(appConfig).Result;
            EnableKeepFreshTimer();
            EnableCheckNeedRecalcTimer();
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
