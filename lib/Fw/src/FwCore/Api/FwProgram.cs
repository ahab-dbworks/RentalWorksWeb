using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace FwCore.Api
{
    public class FwProgram
    {
        public static IWebHostBuilder BuildWebHost(string[] args, Type startupType)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseSetting(WebHostDefaults.DetailedErrorsKey, "true")
                .UseIISIntegration()
                .UseStartup(startupType)
                .CaptureStartupErrors(true);

            return host;
        }
    }
}
