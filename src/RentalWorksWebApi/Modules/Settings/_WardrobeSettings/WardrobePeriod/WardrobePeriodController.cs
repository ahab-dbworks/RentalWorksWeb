using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.WardrobeSettings.WardrobePeriod
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"OhMqm7XJBcfI1")]
    public class WardrobePeriodController : AppDataController
    {
        public WardrobePeriodController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WardrobePeriodLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobeperiod/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"RspO7C50vAeSe", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"CP68gyJjCAdLl", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobeperiod
        [HttpGet]
        [FwControllerMethod(Id:"AVigHeufytKFT", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<WardrobePeriodLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WardrobePeriodLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobeperiod/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"Q5UD7cTskO1hL", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<WardrobePeriodLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WardrobePeriodLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobeperiod
        [HttpPost]
        [FwControllerMethod(Id:"0szM14cnMKjP7", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<WardrobePeriodLogic>> NewAsync([FromBody]WardrobePeriodLogic l)
        {
            return await DoNewAsync<WardrobePeriodLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/wardrobeperio/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "rLYncmtvklrP5", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<WardrobePeriodLogic>> EditAsync([FromRoute] string id, [FromBody]WardrobePeriodLogic l)
        {
            return await DoEditAsync<WardrobePeriodLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/wardrobeperiod/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"fA7U1RLNyJpNv", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<WardrobePeriodLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
