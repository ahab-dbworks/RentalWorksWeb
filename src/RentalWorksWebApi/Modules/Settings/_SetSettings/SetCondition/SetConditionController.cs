using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.SetCondition
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
        [FwControllerMethod(Id:"dqMMuPwDBIc9r")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"eVafjqkCA3Y0C")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/setscondition
        [HttpGet]
        [FwControllerMethod(Id:"ol4O4CHC8wO2C")]
        public async Task<ActionResult<IEnumerable<SetConditionLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SetConditionLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/setscondition/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"dcwh7ytveaoOj")]
        public async Task<ActionResult<SetConditionLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SetConditionLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/setscondition
        [HttpPost]
        [FwControllerMethod(Id:"NT9tDh51JEXtK")]
        public async Task<ActionResult<SetConditionLogic>> PostAsync([FromBody]SetConditionLogic l)
        {
            return await DoPostAsync<SetConditionLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/setscondition/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"4qIpX6lADEW0L")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<SetConditionLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
