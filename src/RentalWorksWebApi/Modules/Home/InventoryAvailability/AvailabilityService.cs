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
        private FwApplicationConfig appConfig;

        //minute timer
        private Timer minuteTimer;
        private int delayMinuteStartSeconds = 2;
        private int minuteSeconds = 60;
        private int lastHourRan = -1;
        private int lastDateRan = -1;

        //check need recalc
        private Timer checkNeedRecalcTimer;
        private int delayCheckNeedRecalcStartSeconds = 2;
        private int checkNeedRecalcSeconds = 3;

        //keep fresh
        private Timer keepFreshTimer;
        private int delayKeepRefreshStartSeconds = 5;
        private int keepFreshSeconds = 15;

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
            minuteTimer = new Timer(EveryMinute, null, TimeSpan.FromSeconds(delayMinuteStartSeconds), TimeSpan.FromSeconds(minuteSeconds));
            bool b = InventoryAvailabilityFunc.InitializeService(appConfig).Result;
            return Task.CompletedTask;
        }
        //-------------------------------------------------------------------------------------------------------
        private void DisableMinuteTimer()
        {
            minuteTimer?.Change(Timeout.Infinite, 0);
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
        private void EnableMinuteTimer()
        {
            minuteTimer?.Change(TimeSpan.FromSeconds(minuteSeconds), TimeSpan.FromSeconds(minuteSeconds));
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
            DisableKeepFreshTimer();
            bool b = InventoryAvailabilityFunc.KeepAvailabilityCacheFresh(appConfig).Result;
            EnableKeepFreshTimer();
        }
        //-------------------------------------------------------------------------------------------------------
        private void EveryMinute(object state)
        {
            DisableMinuteTimer();

            if (DateTime.Now.Hour != lastHourRan)
            {
                lastHourRan = DateTime.Now.Hour;
                InvalidateHourly();
            }
            if (DateTime.Now.Day != lastDateRan)
            {
                lastDateRan = DateTime.Now.Day;
                InvalidateDaily();
            }

            EnableMinuteTimer();
        }
        //-------------------------------------------------------------------------------------------------------
        private void InvalidateHourly()
        {
            bool b = InventoryAvailabilityFunc.InvalidateHourly(appConfig).Result;
        }
        //-------------------------------------------------------------------------------------------------------
        private void InvalidateDaily()
        {
            bool b = InventoryAvailabilityFunc.InvalidateDaily(appConfig).Result;
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
