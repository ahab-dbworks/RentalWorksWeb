using FwCore.Controllers;
using FwStandard.Models;
using Microsoft.Extensions.Options;

namespace WebApi.Controllers
{
    public abstract class AppReportController : FwReportController
    {
        public AppReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
    }
}
