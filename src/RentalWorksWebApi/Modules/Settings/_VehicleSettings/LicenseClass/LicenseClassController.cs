using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.VehicleSettings.LicenseClass
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"gLsGICI8R4VM")]
    public class LicenseClassController : AppDataController
    {
        public LicenseClassController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(LicenseClassLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/licenseclass/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"BlQ5wrGU6eNo", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"NXialy8NL91X", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/licenseclass
        [HttpGet]
        [FwControllerMethod(Id:"rRp6zNLGF5Ka", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<LicenseClassLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<LicenseClassLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/licenseclass/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"Ra8AX63EGEkS", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<LicenseClassLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<LicenseClassLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/licenseclass
        [HttpPost]
        [FwControllerMethod(Id:"WzElPY4qUkRB", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<LicenseClassLogic>> NewAsync([FromBody]LicenseClassLogic l)
        {
            return await DoNewAsync<LicenseClassLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/licenseclas/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "FZpqohpsp9mR7", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<LicenseClassLogic>> EditAsync([FromRoute] string id, [FromBody]LicenseClassLogic l)
        {
            return await DoEditAsync<LicenseClassLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/licenseclass/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"jrJaxkbnqTd7", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<LicenseClassLogic>(id);
        }
        //------------------------------------------------------------------------------------
}
}
