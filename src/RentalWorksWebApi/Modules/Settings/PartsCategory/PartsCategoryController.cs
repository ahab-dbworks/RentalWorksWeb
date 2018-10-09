﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.PartsCategory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class PartsCategoryController : AppDataController
    {
        public PartsCategoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PartsCategoryLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/partscategory/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PartsCategoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/partscategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartsCategoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PartsCategoryLogic>(pageno, pagesize, sort, typeof(PartsCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/partscategory/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<PartsCategoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PartsCategoryLogic>(id, typeof(PartsCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/partscategory
        [HttpPost]
        public async Task<ActionResult<PartsCategoryLogic>> PostAsync([FromBody]PartsCategoryLogic l)
        {
            return await DoPostAsync<PartsCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/partscategory/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(PartsCategoryLogic));
        }
        //------------------------------------------------------------------------------------
    }
}