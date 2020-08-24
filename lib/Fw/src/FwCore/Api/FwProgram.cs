using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace FwCore.Api
{
    public class FwProgram
    {
        public static string ServerVersion = "";
        public static IWebHostBuilder HostBuilder { get; set; } = null;
        public static IWebHost Host { get; set; } = null;
        //---------------------------------------------------------------------------------------------------------------------------
        public static IWebHostBuilder BuildWebHost(string[] args, Type startupType)
        {
            FwProgram.ServerVersion = File.ReadAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "version.txt"));
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseSetting(WebHostDefaults.DetailedErrorsKey, "true")
                .UseIISIntegration()
                .UseStartup(startupType)
                .CaptureStartupErrors(true);

            System.AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            return host;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Console.Error.WriteLine(ex.Message + ex.StackTrace);
        }
        //---------------------------------------------------------------------------------------------------------------------------
    }
}
