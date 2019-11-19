using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.InventorySettings.InventoryStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"eb5WjxGH2duV")]
    public class InventoryStatusController : AppDataController
    {
        public InventoryStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorystatus/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"G0k2Vl6S2Xwg")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"GSucSg8fufoY")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorystatus
        [HttpGet]
        [FwControllerMethod(Id:"fG3aUC2zkHz7")]
        public async Task<ActionResult<IEnumerable<InventoryStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryStatusLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorystatus/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"v1UfzlrrGVO2")]
        public async Task<ActionResult<InventoryStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorystatus
        [HttpPost]
        [FwControllerMethod(Id:"DE8S9AuC0ixt")]
        public async Task<ActionResult<InventoryStatusLogic>> PostAsync([FromBody]InventoryStatusLogic l)
        {
            return await DoPostAsync<InventoryStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/inventorystatus/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"NORjpGMbYjVZ")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventoryStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
