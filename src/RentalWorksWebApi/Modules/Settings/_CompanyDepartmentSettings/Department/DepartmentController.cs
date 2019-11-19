using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Modules.Settings.CompanyDepartmentSettings.Department;

namespace WebApi.Modules.Settings.CompanyDepartmentSettings.CompanyDepartment
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"kuYqT9d6TDEg")]
    public class DepartmentController : AppDataController
    {
        public DepartmentController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DepartmentLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/customertype/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"QaW0TKeQvrCL", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"CvEnpn2Ikq05", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customertype
        [HttpGet]
        [FwControllerMethod(Id:"nOw1UydiCIXJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<DepartmentLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DepartmentLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customertype/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"U96afbggW7oC", ActionType: FwControllerActionTypes.View, ValidateSecurityGroup: false)]
        public async Task<ActionResult<DepartmentLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DepartmentLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customertype
        [HttpPost]
        [FwControllerMethod(Id:"LiekypeTCzjc", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<DepartmentLogic>> NewAsync([FromBody]DepartmentLogic l)
        {
            return await DoNewAsync<DepartmentLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/customertyp/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "ki7WpGfC2cZHn", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<DepartmentLogic>> EditAsync([FromRoute] string id, [FromBody]DepartmentLogic l)
        {
            return await DoEditAsync<DepartmentLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customertype/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"3pAiXb2NT6gG", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<DepartmentLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
