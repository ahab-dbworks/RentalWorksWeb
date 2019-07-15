using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.Warehouse
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"ICJcR2gOu04OB")]
    public class WarehouseController : AppDataController
    {
        public WarehouseController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WarehouseLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/customertype/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"ddZHw4KQLyUAf")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"ppVJpliuTuIgs")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customertype
        [HttpGet]
        [FwControllerMethod(Id:"C6JqXCViTUJ3g")]
        public async Task<ActionResult<IEnumerable<WarehouseLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WarehouseLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customertype/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"3XXkOAC0PQtRD")]
        public async Task<ActionResult<WarehouseLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WarehouseLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customertype
        [HttpPost]
        [FwControllerMethod(Id:"o3XDtND7MjAJM")]
        public async Task<ActionResult<WarehouseLogic>> PostAsync([FromBody]WarehouseLogic l)
        {
            return await DoPostAsync<WarehouseLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customertype/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"bZyYMoAKPXwtF")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<WarehouseLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
