﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Home.DealNote
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class DealNoteController : AppDataController
    {
        public DealNoteController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DealNoteLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealnote/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/dealnote
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DealNoteLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DealNoteLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/dealnote/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<DealNoteLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DealNoteLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealnote
        [HttpPost]
        public async Task<ActionResult<DealNoteLogic>> PostAsync([FromBody]DealNoteLogic l)
        {
            return await DoPostAsync<DealNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/dealnote/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
    }
}