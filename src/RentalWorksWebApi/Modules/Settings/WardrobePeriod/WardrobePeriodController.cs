using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.WardrobePeriod
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"OhMqm7XJBcfI1")]
    public class WardrobePeriodController : AppDataController
    {
        public WardrobePeriodController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WardrobePeriodLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobeperiod/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"RspO7C50vAeSe")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"CP68gyJjCAdLl")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobeperiod
        [HttpGet]
        [FwControllerMethod(Id:"AVigHeufytKFT")]
        public async Task<ActionResult<IEnumerable<WardrobePeriodLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WardrobePeriodLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobeperiod/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"Q5UD7cTskO1hL")]
        public async Task<ActionResult<WardrobePeriodLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WardrobePeriodLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobeperiod
        [HttpPost]
        [FwControllerMethod(Id:"0szM14cnMKjP7")]
        public async Task<ActionResult<WardrobePeriodLogic>> PostAsync([FromBody]WardrobePeriodLogic l)
        {
            return await DoPostAsync<WardrobePeriodLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/wardrobeperiod/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"fA7U1RLNyJpNv")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<WardrobePeriodLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
