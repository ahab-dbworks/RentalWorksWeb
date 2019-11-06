using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.BlackoutStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"1B2gGaIJglY")]
    public class BlackoutStatusController : AppDataController
    {
        public BlackoutStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(BlackoutStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/blackoutstatus/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"bxy3ONGlSyk")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"r0qWX7JcPUL")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/blackoutstatus
        [HttpGet]
        [FwControllerMethod(Id:"tYnzcntGlqo")]
        public async Task<ActionResult<IEnumerable<BlackoutStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<BlackoutStatusLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/blackoutstatus/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"bRtTdiTku8N")]
        public async Task<ActionResult<BlackoutStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<BlackoutStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/blackoutstatus
        [HttpPost]
        [FwControllerMethod(Id:"TasAvZCSso8")]
        public async Task<ActionResult<BlackoutStatusLogic>> PostAsync([FromBody]BlackoutStatusLogic l)
        {
            return await DoPostAsync<BlackoutStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/blackoutstatus/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"95GiHb7P2C7")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<BlackoutStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
