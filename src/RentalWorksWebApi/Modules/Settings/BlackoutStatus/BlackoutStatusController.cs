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
        [Authorize(Policy = "{3EC1D66D-A977-4A7F-8D24-5930A002E63E}")]
        [FwControllerMethod(Id:"bxy3ONGlSyk")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"r0qWX7JcPUL")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/blackoutstatus
        [HttpGet]
        [Authorize(Policy = "{C30C427F-F9DF-41D8-8569-14AD17680624}")]
        [FwControllerMethod(Id:"tYnzcntGlqo")]
        public async Task<ActionResult<IEnumerable<BlackoutStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<BlackoutStatusLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/blackoutstatus/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "{5B6C7CAF-E5E5-45CC-88DF-0AA132F61CE0}")]
        [FwControllerMethod(Id:"bRtTdiTku8N")]
        public async Task<ActionResult<BlackoutStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<BlackoutStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/blackoutstatus
        [HttpPost]
        [Authorize(Policy = "{9B53148A-781B-4CAE-B4F3-AFAAA749A65A}")]
        [FwControllerMethod(Id:"TasAvZCSso8")]
        public async Task<ActionResult<BlackoutStatusLogic>> PostAsync([FromBody]BlackoutStatusLogic l)
        {
            return await DoPostAsync<BlackoutStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/blackoutstatus/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{1BA52854-4796-41B5-973D-9A9731BC4AFE}")]
        [FwControllerMethod(Id:"95GiHb7P2C7")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
