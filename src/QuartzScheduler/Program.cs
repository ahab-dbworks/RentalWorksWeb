using log4net.Config;
using QuartzScheduler.Server;
using System;
using System.IO;
using Topshelf;

namespace QuartzScheduler
{
    static class Program
    {
        /// <summary>
        /// Main.
        /// </summary>
        public static void Main()
        {
            // change from service account's dir to more logical one
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            XmlConfigurator.Configure();

            HostFactory.Run(x =>
            {
                x.RunAsLocalSystem();

                x.SetDescription(Configuration.ServiceDescription);
                x.SetDisplayName(Configuration.ServiceDisplayName);
                x.SetServiceName(Configuration.ServiceName);

                x.Service(factory =>
                {
                    QuartzServer server = QuartzServerFactory.CreateServer();
                    server.Initialize().GetAwaiter().GetResult();
                    return server;
                });
            });
        }
    }
}
