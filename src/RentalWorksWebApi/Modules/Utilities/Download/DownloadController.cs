using FwCore.Controllers;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [AllowAnonymous]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    public class DownloadController : FwDownloadController
    {
        public DownloadController(IOptions<FwApplicationConfig> appConfig): base(appConfig) { }
        //------------------------------------------------------------------------------------
        // GET api/v1/download/filename
        [HttpGet("{filename}")]
        public async Task<IActionResult> GetAsync([FromRoute] string filename, [FromQuery]string downloadasfilename)
        {
            return await base.DoGetAsync(filename, downloadasfilename);
        }
        //------------------------------------------------------------------------------------
    }
}