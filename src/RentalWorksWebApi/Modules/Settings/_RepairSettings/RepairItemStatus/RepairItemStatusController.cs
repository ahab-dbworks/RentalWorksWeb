using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.RepairSettings.RepairItemStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"iuo4dnWX5KCP8")]
    public class RepairItemStatusController : AppDataController
    {
        public RepairItemStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RepairItemStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/repairitemstatus/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"7PISLSsMpgNIl")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"hSwd5NcqOohxR")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/repairitemstatus
        [HttpGet]
        [FwControllerMethod(Id:"Euz7i0djYjPJQ")]
        public async Task<ActionResult<IEnumerable<RepairItemStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RepairItemStatusLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/repairitemstatus/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"wMmh2Y6K2bqxS")]
        public async Task<ActionResult<RepairItemStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RepairItemStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/repairitemstatus
        [HttpPost]
        [FwControllerMethod(Id:"K5NUYW57c4PMY")]
        public async Task<ActionResult<RepairItemStatusLogic>> PostAsync([FromBody]RepairItemStatusLogic l)
        {
            return await DoPostAsync<RepairItemStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/repairitemstatus/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"sgKHiwM1BW35p")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<RepairItemStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
