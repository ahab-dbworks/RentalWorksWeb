using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.ShipVia
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"D1wheIde10lAO")]
    public class ShipViaController : AppDataController
    {
        public ShipViaController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ShipViaLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/shipvia/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"jpwaFdEH4uQxC")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"1qUV3fInUCUvb")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/shipvia
        [HttpGet]
        [FwControllerMethod(Id:"wHPHoZ44MgwlY")]
        public async Task<ActionResult<IEnumerable<ShipViaLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ShipViaLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/shipvia/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"RTeeG4V8I9dfu")]
        public async Task<ActionResult<ShipViaLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ShipViaLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/shipvia
        [HttpPost]
        [FwControllerMethod(Id:"hv2fJKqRAyxV0")]
        public async Task<ActionResult<ShipViaLogic>> PostAsync([FromBody]ShipViaLogic l)
        {
            return await DoPostAsync<ShipViaLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/shipvia/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"SBcQx7ZsPd7u9")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ShipViaLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
