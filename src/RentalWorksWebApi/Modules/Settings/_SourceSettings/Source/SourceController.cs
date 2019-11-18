using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.Source
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"BOH4LAvrGvVjW")]
    public class SourceController : AppDataController
    {
        public SourceController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SourceLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/source/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"41ltoSAdOiT26")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"IjQhH4oFfzgjP")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/source
        [HttpGet]
        [FwControllerMethod(Id:"4EO99ztxaXWPQ")]
        public async Task<ActionResult<IEnumerable<SourceLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SourceLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/source/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"w9RNfxyLXwVaT")]
        public async Task<ActionResult<SourceLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SourceLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/source
        [HttpPost]
        [FwControllerMethod(Id:"RcWQIbO6VOptL")]
        public async Task<ActionResult<SourceLogic>> PostAsync([FromBody]SourceLogic l)
        {
            return await DoPostAsync<SourceLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/source/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"8hzZe0mNa1Unh")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<SourceLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
