using FwCore.Api;
using Microsoft.AspNetCore.Hosting;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHostBuilder hostBuilder = FwProgram.BuildWebHost(args, typeof(Startup));
            IWebHost host = hostBuilder.Build();
            host.Run();
        }
    }
}
