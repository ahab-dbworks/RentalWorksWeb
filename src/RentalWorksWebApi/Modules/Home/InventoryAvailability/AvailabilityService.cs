using FwStandard.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Modules.Settings.AvailabilitySettings;

namespace WebApi.Modules.Home.InventoryAvailability
{
    internal class AvailabilityService : IHostedService, IDisposable
    {
        private FwApplicationConfig appConfig;
        private AvailabilitySettingsLogic availSettings;
        private const int TIMER_DELAY_START = 2;     //delay all timers to allow service to start before timers kick in
        private const int SECONDS_PER_MINUTE = 60;

        //minute timer
        private Timer minuteTimer;
        private int lastHourRan = -1;
        private int lastDateRan = -1;

        //check need recalc
        private Timer checkNeedRecalcTimer;

        //keep fresh
        private Timer keepFreshTimer;

        //-------------------------------------------------------------------------------------------------------
        public AvailabilityService(IOptions<FwApplicationConfig> appConfig)
        {
            this.appConfig = appConfig.Value;
        }
        //-------------------------------------------------------------------------------------------------------
        public Task StartAsync(CancellationToken cancellationToken)
        {
            bool b = false;

            // initialize "need recalc index".  initialize constants
            b = InventoryAvailabilityFunc.InitializeService(appConfig).Result;

            availSettings = new AvailabilitySettingsLogic();
            availSettings.SetDependencies(appConfig, null);
            availSettings.ControlId = "1";
            b = availSettings.LoadAsync<AvailabilitySettingsLogic>().Result;

            //timer ticks every 60 seconds in order to detect change in Hours or Days
            minuteTimer = new Timer(EveryMinute, null, TimeSpan.FromSeconds(TIMER_DELAY_START), TimeSpan.FromSeconds(SECONDS_PER_MINUTE));

            //timer tickes every 3 seconds to detect changes to Inventory caused by other systems (QuikScan, desktop EXE, database triggers, etc)
            checkNeedRecalcTimer = new Timer(CheckNeedRecalc, null, TimeSpan.FromSeconds(TIMER_DELAY_START), TimeSpan.FromSeconds(availSettings.PollForStaleAvailabilitySeconds.GetValueOrDefault(0)));

            //if enabled, this timer will tick ever XX seconds and attempt to keep the availability cache current
            if ((availSettings.KeepAvailabilityCacheCurrent.GetValueOrDefault(false)) && (availSettings.KeepCurrentSeconds.GetValueOrDefault(0) > 0))
            {
                keepFreshTimer = new Timer(KeepFresh, null, TimeSpan.FromSeconds(TIMER_DELAY_START), TimeSpan.FromSeconds(availSettings.KeepCurrentSeconds.GetValueOrDefault(0)));
            }
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
            minuteTimer?.Change(TimeSpan.FromSeconds(SECONDS_PER_MINUTE), TimeSpan.FromSeconds(SECONDS_PER_MINUTE));
        }
        //-------------------------------------------------------------------------------------------------------
        private void EnableKeepFreshTimer()
        {
            keepFreshTimer?.Change(TimeSpan.FromSeconds(availSettings.KeepCurrentSeconds.GetValueOrDefault(0)), TimeSpan.FromSeconds(availSettings.KeepCurrentSeconds.GetValueOrDefault(0)));
        }
        //-------------------------------------------------------------------------------------------------------
        private void EnableCheckNeedRecalcTimer()
        {
            checkNeedRecalcTimer?.Change(TimeSpan.FromSeconds(availSettings.PollForStaleAvailabilitySeconds.GetValueOrDefault(0)), TimeSpan.FromSeconds(availSettings.PollForStaleAvailabilitySeconds.GetValueOrDefault(0)));
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
