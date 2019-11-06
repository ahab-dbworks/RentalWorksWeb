using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.CreditStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"A4P8o1quoutj")]
    public class CreditStatusController : AppDataController
    {
        public CreditStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CreditStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/creditstatus/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"hCwOik981Yqj")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"aubeGN1TlWVp")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/creditstatus
        [HttpGet]
        [FwControllerMethod(Id:"0b6Wvuxdclab")]
        public async Task<ActionResult<IEnumerable<CreditStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CreditStatusLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/creditstatus/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"utOuF6My0pO8")]
        public async Task<ActionResult<CreditStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CreditStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/creditstatus
        [HttpPost]
        [FwControllerMethod(Id:"ZzVEAuLidFfR")]
        public async Task<ActionResult<CreditStatusLogic>> PostAsync([FromBody]CreditStatusLogic l)
        {
            return await DoPostAsync<CreditStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/creditstatus/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"u2ulnVktn1uJ")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CreditStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
