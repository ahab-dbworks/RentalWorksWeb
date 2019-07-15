using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.InventoryAdjustmentReason
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"geoncGlrjrAr")]
    public class InventoryAdjustmentReasonController : AppDataController
    {
        public InventoryAdjustmentReasonController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryAdjustmentReasonLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventoryadjustmentreason/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"90WCOieJfP5u")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"ZIDZmBWvrZWX")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventoryadjustmentreason
        [HttpGet]
        [FwControllerMethod(Id:"1KReANHiQNTL")]
        public async Task<ActionResult<IEnumerable<InventoryAdjustmentReasonLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryAdjustmentReasonLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventoryadjustmentreason/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"a2fLO3J2QLDY")]
        public async Task<ActionResult<InventoryAdjustmentReasonLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryAdjustmentReasonLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventoryadjustmentreason
        [HttpPost]
        [FwControllerMethod(Id:"xF3LX1mpDsiG")]
        public async Task<ActionResult<InventoryAdjustmentReasonLogic>> PostAsync([FromBody]InventoryAdjustmentReasonLogic l)
        {
            return await DoPostAsync<InventoryAdjustmentReasonLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/inventoryadjustmentreason/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"w1HPTxZK8VYh")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventoryAdjustmentReasonLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
