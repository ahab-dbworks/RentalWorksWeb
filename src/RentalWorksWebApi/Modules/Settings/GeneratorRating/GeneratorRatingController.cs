using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.GeneratorRating
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"B0hMFj9s7XGT")]
    public class GeneratorRatingController : AppDataController
    {
        public GeneratorRatingController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(GeneratorRatingLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatorrating/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"I481QnyX57Xx")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"fC2pI0mBVTMU")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatorrating
        [HttpGet]
        [FwControllerMethod(Id:"03fxxP1iuQBc")]
        public async Task<ActionResult<IEnumerable<GeneratorRatingLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GeneratorRatingLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatorrating/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"C6ypz6FwV7il")]
        public async Task<ActionResult<GeneratorRatingLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<GeneratorRatingLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatorrating
        [HttpPost]
        [FwControllerMethod(Id:"WXbrUWfWD4gs")]
        public async Task<ActionResult<GeneratorRatingLogic>> PostAsync([FromBody]GeneratorRatingLogic l)
        {
            return await DoPostAsync<GeneratorRatingLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/generatorrating/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"8NNpylgBHqIL")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<GeneratorRatingLogic>(id);
        }
        //------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------
}
}
