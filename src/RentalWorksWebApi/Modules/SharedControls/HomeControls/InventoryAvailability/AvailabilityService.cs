using FwStandard.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Modules.Settings.SystemSettings.AvailabilitySettings;
using WebApi;

namespace WebApi.Modules.HomeControls.InventoryAvailability
{
    //internal class AvailabilityService : IHostedService, IDisposable
    public class AvailabilityService : IHostedService, IDisposable
    {
        private FwApplicationConfig appConfig;
        private static AvailabilitySettingsLogic availSettings;
        private const int SECONDS_PER_MINUTE = 60;

        //minute timer
        private System.Timers.Timer minuteTimer;
        private int lastHourRan = -1;
        private int lastDateRan = -1;

        //check need recalc
        private static System.Timers.Timer checkNeedRecalcTimer;

        //keep fresh
        private static System.Timers.Timer keepFreshTimer;

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
            availSettings.ControlId = RwConstants.CONTROL_ID;
            b = availSettings.LoadAsync<AvailabilitySettingsLogic>().Result;

            //timer ticks every 60 seconds in order to detect change in Hours or Days
            minuteTimer = new System.Timers.Timer(1000 * SECONDS_PER_MINUTE);
            minuteTimer.Elapsed += MinuteTimer_Elapsed;
            minuteTimer.Start();

            //timer tickes every 3 seconds to detect changes to Inventory caused by other systems (QuikScan, desktop EXE, database triggers, etc)
            checkNeedRecalcTimer = new System.Timers.Timer(1000 * availSettings.PollForStaleAvailabilitySeconds.GetValueOrDefault(0));
            checkNeedRecalcTimer.Elapsed += CheckNeedRecalcTimer_Elapsed;
            EnableCheckNeedRecalcTimer();

            //if enabled, this timer will tick ever XX seconds and attempt to keep the availability cache current
            if ((availSettings.KeepAvailabilityCacheCurrent.GetValueOrDefault(false)) && (availSettings.KeepCurrentSeconds.GetValueOrDefault(0) > 0))
            {
                keepFreshTimer = new System.Timers.Timer(1000 * availSettings.KeepCurrentSeconds.GetValueOrDefault(0));
                keepFreshTimer.Elapsed += KeepFreshTimer_Elapsed;
                EnableKeepFreshTimer();
            }
            return Task.CompletedTask;
        }
        //-------------------------------------------------------------------------------------------------------
        public static void SetPollForStaleAvailabilitySeconds(int? seconds)
        {
            availSettings.PollForStaleAvailabilitySeconds = seconds.GetValueOrDefault(0);
            if (checkNeedRecalcTimer.Enabled)
            {
                DisableCheckNeedRecalcTimer();
                EnableCheckNeedRecalcTimer();
            }
        }
        //-------------------------------------------------------------------------------------------------------
        public static void SetKeepCurrent(bool? keepCurrent)
        {
            if (!availSettings.KeepAvailabilityCacheCurrent.GetValueOrDefault(false) && keepCurrent.GetValueOrDefault(false))
            {
                EnableKeepFreshTimer();
            }
            else if (availSettings.KeepAvailabilityCacheCurrent.GetValueOrDefault(false) && !keepCurrent.GetValueOrDefault(false))
            {
                DisableKeepFreshTimer();
            }
            availSettings.KeepAvailabilityCacheCurrent = keepCurrent.GetValueOrDefault(false);
        }
        //-------------------------------------------------------------------------------------------------------
        public static void SetKeepCurrentSeconds(int? seconds)
        {
            availSettings.KeepCurrentSeconds = seconds.GetValueOrDefault(0);
            if (keepFreshTimer.Enabled)
            {
                DisableKeepFreshTimer();
                EnableKeepFreshTimer();
            }
        }
        //-------------------------------------------------------------------------------------------------------
        private void DisableMinuteTimer()
        {
            minuteTimer?.Stop();
        }
        //-------------------------------------------------------------------------------------------------------
        private static void DisableKeepFreshTimer()
        {
            keepFreshTimer?.Stop();
        }
        //-------------------------------------------------------------------------------------------------------
        private static void DisableCheckNeedRecalcTimer()
        {
            checkNeedRecalcTimer?.Stop();
        }
        //-------------------------------------------------------------------------------------------------------
        private void EnableMinuteTimer()
        {
            minuteTimer?.Start();
        }
        //-------------------------------------------------------------------------------------------------------
        private static void EnableKeepFreshTimer()
        {
            keepFreshTimer.Interval = 1000 * availSettings.KeepCurrentSeconds.GetValueOrDefault(0);
            keepFreshTimer?.Start();
        }
        //-------------------------------------------------------------------------------------------------------
        private static void EnableCheckNeedRecalcTimer()
        {
            checkNeedRecalcTimer.Interval = 1000 * availSettings.PollForStaleAvailabilitySeconds.GetValueOrDefault(0);
            checkNeedRecalcTimer?.Start();
        }
        //-------------------------------------------------------------------------------------------------------
        private void MinuteTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
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
            }
            finally
            {
                EnableMinuteTimer();
            }
        }
        //-------------------------------------------------------------------------------------------------------
        private void KeepFreshTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                DisableKeepFreshTimer();
                bool b = InventoryAvailabilityFunc.KeepAvailabilityCacheFresh(appConfig).Result;
            }
            finally

            {
                EnableKeepFreshTimer();
            }
        }
        //-------------------------------------------------------------------------------------------------------
        private void CheckNeedRecalcTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                DisableCheckNeedRecalcTimer();
                bool b = InventoryAvailabilityFunc.CheckNeedRecalc(appConfig).Result;
            }
            finally
            {
                EnableCheckNeedRecalcTimer();
            }
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
