using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.CustomerSettings.CreditStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"A4P8o1quoutj")]
    public class CreditStatusController : AppDataController
    {
        public CreditStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CreditStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/creditstatus/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"hCwOik981Yqj", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"aubeGN1TlWVp", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/creditstatus
        [HttpGet]
        [FwControllerMethod(Id:"0b6Wvuxdclab", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<CreditStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CreditStatusLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/creditstatus/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"utOuF6My0pO8", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<CreditStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CreditStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/creditstatus
        [HttpPost]
        [FwControllerMethod(Id:"ZzVEAuLidFfR", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<CreditStatusLogic>> NewAsync([FromBody]CreditStatusLogic l)
        {
            return await DoNewAsync<CreditStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/creditstatu/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "0HxlB4t61Ges1", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<CreditStatusLogic>> EditAsync([FromRoute] string id, [FromBody]CreditStatusLogic l)
        {
            return await DoEditAsync<CreditStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/creditstatus/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"u2ulnVktn1uJ", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CreditStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
