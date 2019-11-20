using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.FacilitySettings.FacilityScheduleStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"8QjijODAMJt")]
    public class FacilityScheduleStatusController : AppDataController
    {
        public FacilityScheduleStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(FacilityScheduleStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilityschedulestatus/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"ssoioXCBXvv", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"8diYlVvoxMJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilityschedulestatus
        [HttpGet]
        [FwControllerMethod(Id:"FeVOPc3rVWl", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<FacilityScheduleStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FacilityScheduleStatusLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilityschedulestatus/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"4hMPg1cyEfR", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<FacilityScheduleStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<FacilityScheduleStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilityschedulestatus
        [HttpPost]
        [FwControllerMethod(Id:"aSfogjb33RJ", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<FacilityScheduleStatusLogic>> NewAsync([FromBody]FacilityScheduleStatusLogic l)
        {
            return await DoNewAsync<FacilityScheduleStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/facilityschedulestatu/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "R0GYoXTlx6PmH", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<FacilityScheduleStatusLogic>> EditAsync([FromRoute] string id, [FromBody]FacilityScheduleStatusLogic l)
        {
            return await DoEditAsync<FacilityScheduleStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/facilityschedulestatus/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"kIqN9KW23ul", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<FacilityScheduleStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
