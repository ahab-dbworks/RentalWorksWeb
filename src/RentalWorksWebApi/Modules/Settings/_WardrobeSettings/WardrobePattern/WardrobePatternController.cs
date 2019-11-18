using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.WardrobePattern
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"3SMTlxYJ2LGH1")]
    public class WardrobePatternController : AppDataController
    {
        public WardrobePatternController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WardrobePatternLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobepattern/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"XBzTtIfPAFVgZ")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"f6YLfzMW4HTif")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobepattern
        [HttpGet]
        [FwControllerMethod(Id:"rBOh7oshuTBR6")]
        public async Task<ActionResult<IEnumerable<WardrobePatternLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WardrobePatternLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobepattern/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"5qRsdDBX7V0uX")]
        public async Task<ActionResult<WardrobePatternLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WardrobePatternLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobepattern
        [HttpPost]
        [FwControllerMethod(Id:"905S7t2LYfmI6")]
        public async Task<ActionResult<WardrobePatternLogic>> PostAsync([FromBody]WardrobePatternLogic l)
        {
            return await DoPostAsync<WardrobePatternLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/wardrobepattern/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"W3MNIb72R6jV5")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<WardrobePatternLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
