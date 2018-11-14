using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.Department
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
        [Authorize(Policy = "{EE8AEB08-C76C-4C7A-BC44-701306F7EDE2}")]
        [FwControllerMethod(Id:"QaW0TKeQvrCL")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"CvEnpn2Ikq05")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customertype
        [HttpGet]
        [Authorize(Policy = "{FA2F2119-F187-4F94-A6BA-62EF1DE5892B}")]
        [FwControllerMethod(Id:"nOw1UydiCIXJ")]
        public async Task<ActionResult<IEnumerable<DepartmentLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DepartmentLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customertype/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "{562375F5-18F6-4A11-8B91-F9185988358B}")]
        [FwControllerMethod(Id:"U96afbggW7oC")]
        public async Task<ActionResult<DepartmentLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DepartmentLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customertype
        [HttpPost]
        [Authorize(Policy = "{032B5C1D-B986-47F2-BE91-FC3FBF63319A}")]
        [FwControllerMethod(Id:"LiekypeTCzjc")]
        public async Task<ActionResult<DepartmentLogic>> PostAsync([FromBody]DepartmentLogic l)
        {
            return await DoPostAsync<DepartmentLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customertype/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{A5FB724E-DCC6-49C8-B15F-625C902303ED}")]
        [FwControllerMethod(Id:"3pAiXb2NT6gG")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
