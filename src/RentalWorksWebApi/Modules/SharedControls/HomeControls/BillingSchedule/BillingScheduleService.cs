using FwStandard.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Modules.HomeControls.BillingSchedule
{
    internal class BillingScheduleService : IHostedService, IDisposable
    {
        private FwApplicationConfig appConfig;
        private const int CHECK_INTERVAL_SECONDS = 10;

        //keep fresh
        private System.Timers.Timer keepFreshTimer;

        //-------------------------------------------------------------------------------------------------------
        public BillingScheduleService(IOptions<FwApplicationConfig> appConfig)
        {
            this.appConfig = appConfig.Value;
        }
        //-------------------------------------------------------------------------------------------------------
        public Task StartAsync(CancellationToken cancellationToken)
        {
            keepFreshTimer = new System.Timers.Timer(1000 * CHECK_INTERVAL_SECONDS);
            keepFreshTimer.Elapsed += KeepFreshTimer_Elapsed;
            keepFreshTimer.Start();
            return Task.CompletedTask;
        }
        //-------------------------------------------------------------------------------------------------------
        private void DisableKeepFreshTimer()
        {
            keepFreshTimer?.Stop();
        }
        //-------------------------------------------------------------------------------------------------------
        private void EnableKeepFreshTimer()
        {
            keepFreshTimer.Interval = 1000 * CHECK_INTERVAL_SECONDS;
            keepFreshTimer?.Start();
        }
        //-------------------------------------------------------------------------------------------------------
        private void KeepFreshTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                DisableKeepFreshTimer();
                bool b = BillingScheduleFunc.KeepBillingScheduleCacheFresh(appConfig).Result;
            }
            finally

            {
                EnableKeepFreshTimer();
            }
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
