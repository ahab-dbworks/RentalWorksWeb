using FwStandard.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FwCore.Services.EmailService
{
    public abstract class FwEmailService : IHostedService, IDisposable
    {
        protected FwApplicationConfig appConfig;

        //processEmailTimer
        private System.Timers.Timer processEmailTimer;
        private bool isRunning = false;
        //-------------------------------------------------------------------------------------------------------
        public FwEmailService(IOptions<FwApplicationConfig> appConfig)
        {
            this.appConfig = appConfig.Value;
        }
        //-------------------------------------------------------------------------------------------------------
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //timer ticks every 60 seconds in order to detect change in Hours or Days
            //minuteTimer = new Timer(EveryMinute, null, TimeSpan.FromSeconds(TIMER_DELAY_START), TimeSpan.FromSeconds(SECONDS_PER_MINUTE));
            //await this.ProcessEmailAsync();
            processEmailTimer = new System.Timers.Timer(1000 * 60);
            processEmailTimer.Elapsed += ProcessEmailTimer_Elapsed;
            processEmailTimer.Start();
            await Task.CompletedTask;
        }
        //-------------------------------------------------------------------------------------------------------
        private void DisableProcessEmailTimer()
        {
            processEmailTimer?.Stop();
        }
        //-------------------------------------------------------------------------------------------------------
        private void EnableProcessEmailTimer()
        {
            processEmailTimer?.Start();
        }
        //-------------------------------------------------------------------------------------------------------
        private async void ProcessEmailTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (!this.isRunning)
                {
                    this.isRunning = true;
                    DisableProcessEmailTimer();
                    Func<BeforeSendEmailRequest, Task<BeforeSendEmailResponse>> funcBeforeSendEmail = this.BeforeSendEmail;
                    await FwEmailServiceUtil.ProcessEmailAsync(this.appConfig, funcBeforeSendEmail);
                }
            }
            finally
            {
                this.isRunning = false;
                EnableProcessEmailTimer();
            }
        }
        //-------------------------------------------------------------------------------------------------------
        public abstract Task<BeforeSendEmailResponse> BeforeSendEmail(BeforeSendEmailRequest request);
        //-------------------------------------------------------------------------------------------------------
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            DisableProcessEmailTimer();
            await Task.CompletedTask;
        }
        //-------------------------------------------------------------------------------------------------------
        public void Dispose()
        {
            processEmailTimer?.Dispose();
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
