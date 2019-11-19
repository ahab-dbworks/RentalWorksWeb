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
        [FwControllerMethod(Id:"G0k2Vl6S2Xwg", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"GSucSg8fufoY", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorystatus
        [HttpGet]
        [FwControllerMethod(Id:"fG3aUC2zkHz7", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<InventoryStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryStatusLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorystatus/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"v1UfzlrrGVO2", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<InventoryStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorystatus
        [HttpPost]
        [FwControllerMethod(Id:"DE8S9AuC0ixt", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<InventoryStatusLogic>> NewAsync([FromBody]InventoryStatusLogic l)
        {
            return await DoNewAsync<InventoryStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/inventorystatu/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "QAzi9Fwh64Qxk", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<InventoryStatusLogic>> EditAsync([FromRoute] string id, [FromBody]InventoryStatusLogic l)
        {
            return await DoEditAsync<InventoryStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/inventorystatus/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"NORjpGMbYjVZ", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventoryStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
