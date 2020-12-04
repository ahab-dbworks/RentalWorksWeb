using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.AccountServices.HubSpot;

namespace WebApi.Modules.Administrator.Plugin
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id: "TcXQ0Mt5L0Rf")]
    public class PluginController : AppDataController
    {
        public PluginController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PluginLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/plugin/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id: "2j7gFxJjQLYw", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/plugin/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "AHWsArbEBBGg", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/plugin
        [HttpGet]
        [FwControllerMethod(Id: "DDVLV0fYc95V", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<PluginLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<PluginLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/plugin/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "buHeU7c1Fq5L", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<PluginLogic>> GetOneAsync(string id)
        {
            return await DoGetAsync<PluginLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/plugin
        [HttpPost]
        [FwControllerMethod(Id: "1PIDbTYAL1Mc", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<PluginLogic>> NewAsync([FromBody] PluginLogic l)
        {
            return await DoNewAsync<PluginLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/plugin/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "L7x0GatBLarf", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<PluginLogic>> EditAsync([FromRoute] string id, [FromBody] PluginLogic l)
        {
            return await DoEditAsync<PluginLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/plugin/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "T7TDyPLnHkUf", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync<PluginLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
