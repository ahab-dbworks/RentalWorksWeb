﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.PoClassification
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class PoClassificationController : AppDataController
    {
        public PoClassificationController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PoClassificationLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/poclassification/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PoClassificationLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/poclassification
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PoClassificationLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PoClassificationLogic>(pageno, pagesize, sort, typeof(PoClassificationLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/poclassification/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<PoClassificationLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PoClassificationLogic>(id, typeof(PoClassificationLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/poclassification
        [HttpPost]
        public async Task<ActionResult<PoClassificationLogic>> PostAsync([FromBody]PoClassificationLogic l)
        {
            return await DoPostAsync<PoClassificationLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/poclassification/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(PoClassificationLogic));
        }
        //------------------------------------------------------------------------------------
    }
}