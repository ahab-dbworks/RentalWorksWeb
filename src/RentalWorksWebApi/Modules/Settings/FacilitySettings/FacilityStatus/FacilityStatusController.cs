using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.FacilitySettings.FacilityStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"xJ4UyFe61kC")]
    public class FacilityStatusController : AppDataController
    {
        public FacilityStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(FacilityStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilitystatus/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"pag3dNi989O", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"qpcDK3gVTsd", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilitystatus
        [HttpGet]
        [FwControllerMethod(Id:"xYIDDOLzdho", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<FacilityStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FacilityStatusLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilitystatus/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"Iyz07pUTcsp", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<FacilityStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<FacilityStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilitystatus
        [HttpPost]
        [FwControllerMethod(Id:"aNERQ33ltbW", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<FacilityStatusLogic>> NewAsync([FromBody]FacilityStatusLogic l)
        {
            return await DoNewAsync<FacilityStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/facilitystatu/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "57s2Wq8nAf452", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<FacilityStatusLogic>> EditAsync([FromRoute] string id, [FromBody]FacilityStatusLogic l)
        {
            return await DoEditAsync<FacilityStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/facilitystatus/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"0jKhVmj9NiS", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<FacilityStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
