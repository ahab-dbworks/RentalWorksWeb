using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.InventoryCondition
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"JL0j4lk1KfBY")]
    public class InventoryConditionController : AppDataController
    {
        public InventoryConditionController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryConditionLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorycondition/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"jDQ5tPlefkH4")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"Sxx4UVfhRBYu")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorycondition
        [HttpGet]
        [FwControllerMethod(Id:"zgai4Qixt3Iv")]
        public async Task<ActionResult<IEnumerable<InventoryConditionLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryConditionLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorycondition/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"z0xLp0pb1Js1")]
        public async Task<ActionResult<InventoryConditionLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryConditionLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorycondition
        [HttpPost]
        [FwControllerMethod(Id:"uwuXm3smW48v")]
        public async Task<ActionResult<InventoryConditionLogic>> PostAsync([FromBody]InventoryConditionLogic l)
        {
            return await DoPostAsync<InventoryConditionLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/inventorycondition/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"DHTyby6tWicz")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventoryConditionLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
