using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.ShipViaSettings.ShipVia
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
        [FwControllerMethod(Id:"jpwaFdEH4uQxC", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"1qUV3fInUCUvb", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/shipvia
        [HttpGet]
        [FwControllerMethod(Id:"wHPHoZ44MgwlY", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<ShipViaLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ShipViaLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/shipvia/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"RTeeG4V8I9dfu", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<ShipViaLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ShipViaLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/shipvia
        [HttpPost]
        [FwControllerMethod(Id:"hv2fJKqRAyxV0", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<ShipViaLogic>> NewAsync([FromBody]ShipViaLogic l)
        {
            return await DoNewAsync<ShipViaLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/shipvi/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "F4thd7w1GWxyY", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<ShipViaLogic>> EditAsync([FromRoute] string id, [FromBody]ShipViaLogic l)
        {
            return await DoEditAsync<ShipViaLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/shipvia/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"SBcQx7ZsPd7u9", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ShipViaLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
