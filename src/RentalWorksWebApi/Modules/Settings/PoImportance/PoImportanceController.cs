using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.PoImportance
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"gLt2YTtB2afl")]
    public class PoImportanceController : AppDataController
    {
        public PoImportanceController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PoImportanceLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/poimportance/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"KOVgdvx5a3Ya")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"l3c00lsxhSyh")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/poimportance
        [HttpGet]
        [FwControllerMethod(Id:"m3yvAg6R0B8Q")]
        public async Task<ActionResult<IEnumerable<PoImportanceLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PoImportanceLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/poimportance/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"lMZFvAGmlODB")]
        public async Task<ActionResult<PoImportanceLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PoImportanceLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/poimportance
        [HttpPost]
        [FwControllerMethod(Id:"JnMyfRIJCxUF")]
        public async Task<ActionResult<PoImportanceLogic>> PostAsync([FromBody]PoImportanceLogic l)
        {
            return await DoPostAsync<PoImportanceLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/poimportance/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"hRhHLAqtxFKq")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PoImportanceLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
