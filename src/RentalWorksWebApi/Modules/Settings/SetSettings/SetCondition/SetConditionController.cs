using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.SetSettings.SetCondition
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"3r7dQtlxlUX8u")]
    public class SetConditionController : AppDataController
    {
        public SetConditionController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SetConditionLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/setscondition/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"dqMMuPwDBIc9r", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"eVafjqkCA3Y0C", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/setscondition
        [HttpGet]
        [FwControllerMethod(Id:"ol4O4CHC8wO2C", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<SetConditionLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SetConditionLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/setscondition/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"dcwh7ytveaoOj", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<SetConditionLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SetConditionLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/setscondition
        [HttpPost]
        [FwControllerMethod(Id:"NT9tDh51JEXtK", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<SetConditionLogic>> NewAsync([FromBody]SetConditionLogic l)
        {
            return await DoNewAsync<SetConditionLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/setsconditio/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "uGO0FYS99K4LG", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<SetConditionLogic>> EditAsync([FromRoute] string id, [FromBody]SetConditionLogic l)
        {
            return await DoEditAsync<SetConditionLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/setscondition/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"4qIpX6lADEW0L", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<SetConditionLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
