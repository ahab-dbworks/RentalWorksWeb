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
    public class InventoryTypeController : AppDataController
    {
        public InventoryTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorytype/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorytype
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryTypeLogic>(pageno, pagesize, sort, typeof(InventoryTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorytype/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryTypeLogic>(id, typeof(InventoryTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorytype
        [HttpPost]
        public async Task<ActionResult<InventoryTypeLogic>> PostAsync([FromBody]InventoryTypeLogic l)
        {
            return await DoPostAsync<InventoryTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/inventorytype/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventoryTypeLogic));
        }
        //------------------------------------------------------------------------------------
    }
}