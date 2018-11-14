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
        [Authorize(Policy = "{915BC77B-88C6-4DF7-9AC8-9B2CC0B2ECDB}")]
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
        [Authorize(Policy = "{D014CE24-1518-4022-9D66-E779DF284AB4}")]
        [FwControllerMethod(Id:"03fxxP1iuQBc")]
        public async Task<ActionResult<IEnumerable<GeneratorRatingLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GeneratorRatingLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatorrating/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "{10B3F329-73CE-4359-8DF2-CF86DF76C9E4}")]
        [FwControllerMethod(Id:"C6ypz6FwV7il")]
        public async Task<ActionResult<GeneratorRatingLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<GeneratorRatingLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatorrating
        [HttpPost]
        [Authorize(Policy = "{53DD87EE-08DA-4FE5-A2CC-EE8C38DA40C8}")]
        [FwControllerMethod(Id:"WXbrUWfWD4gs")]
        public async Task<ActionResult<GeneratorRatingLogic>> PostAsync([FromBody]GeneratorRatingLogic l)
        {
            return await DoPostAsync<GeneratorRatingLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/generatorrating/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{7E849D41-933F-4715-9F77-4FDB6278A138}")]
        [FwControllerMethod(Id:"8NNpylgBHqIL")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------
}
}
