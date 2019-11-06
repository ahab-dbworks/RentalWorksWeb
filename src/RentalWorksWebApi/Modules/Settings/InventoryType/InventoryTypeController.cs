using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.InventoryType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"aFLFxVNukHJt")]
    public class InventoryTypeController : AppDataController
    {
        public InventoryTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorytype/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"lL61epuHQalZ")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"MHyVZ85JI8M8")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorytype
        [HttpGet]
        [FwControllerMethod(Id:"ykR4x0wFMUn7")]
        public async Task<ActionResult<IEnumerable<InventoryTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorytype/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"Ce4lIyWC6DED")]
        public async Task<ActionResult<InventoryTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorytype
        [HttpPost]
        [FwControllerMethod(Id:"X9DLhKYWzLDt")]
        public async Task<ActionResult<InventoryTypeLogic>> PostAsync([FromBody]InventoryTypeLogic l)
        {
            return await DoPostAsync<InventoryTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/inventorytype/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"KrYwO2Kkk0pL")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventoryTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
