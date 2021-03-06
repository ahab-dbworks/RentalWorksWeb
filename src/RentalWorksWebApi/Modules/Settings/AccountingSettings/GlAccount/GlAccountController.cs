using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.AccountingSettings.GlAccount
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"1bUgvfRlo7v4")]
    public class GlAccountController : AppDataController
    {
        public GlAccountController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(GlAccountLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/glaccount/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"3xZ4qPvZPFD1", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"y4XYEm4URFCv", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/glaccount
        [HttpGet]
        [FwControllerMethod(Id:"Pa7788wvw8Fe", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<GlAccountLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GlAccountLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/glaccount/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"J1WSh63wd5ud", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<GlAccountLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<GlAccountLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/glaccount
        [HttpPost]
        [FwControllerMethod(Id:"5COnSSyR3QJx", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<GlAccountLogic>> NewAsync([FromBody]GlAccountLogic l)
        {
            return await DoNewAsync<GlAccountLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/glaccoun/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "PVBQnQLbmmGhY", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<GlAccountLogic>> EditAsync([FromRoute] string id, [FromBody]GlAccountLogic l)
        {
            return await DoEditAsync<GlAccountLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/glaccount/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"Ci3O5l0xJGVT", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<GlAccountLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
