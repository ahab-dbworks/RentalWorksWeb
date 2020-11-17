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
        private const int TIMER_DELAY_START = 2;     //delay all timers to allow service to start before timers kick in
        private const int SECONDS_PER_MINUTE = 60;

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
            keepFreshTimer = new System.Timers.Timer(1000 * 10);
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
            keepFreshTimer.Interval = 1000 * 10;
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
