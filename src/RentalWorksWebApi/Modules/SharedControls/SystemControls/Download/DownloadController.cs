using FwStandard.AppManager;
﻿using FwCore.Controllers;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace WebApi.Modules.SharedControls.Download
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    //[FwController(Id: "uF40evC7y2aq")]
    public class DownloadController : FwDownloadController
    {
        public DownloadController(IOptions<FwApplicationConfig> appConfig): base(appConfig) { }
        //------------------------------------------------------------------------------------
        // GET api/v1/download/filename
        [HttpGet("{filename}")]
        [FwControllerMethod(Id:"bUOZ0KtICvIm", ActionType: FwControllerActionTypes.Browse, AllowAnonymous: true)]
        public async Task<ActionResult<FileStreamResult>> GetOneAsync([FromRoute] string filename, [FromQuery]string downloadasfilename)
        {
            return await base.DoGetAsync(filename, downloadasfilename);
        }
        //------------------------------------------------------------------------------------
    }
}
