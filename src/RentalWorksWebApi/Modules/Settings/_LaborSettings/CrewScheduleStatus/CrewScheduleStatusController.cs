using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.LaborSettings.CrewScheduleStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"c0X4YfdKCp06")]
    public class CrewScheduleStatusController : AppDataController
    {
        public CrewScheduleStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CrewScheduleStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/crewschedulestatus/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"h7Q7ljTeHbzr", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"h8UKza6Klxiw", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/crewschedulestatus
        [HttpGet]
        [FwControllerMethod(Id:"Hxhe4I8NGRO4", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<CrewScheduleStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CrewScheduleStatusLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/crewschedulestatus/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"bpOKuEY7KQtq", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<CrewScheduleStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CrewScheduleStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/crewschedulestatus
        [HttpPost]
        [FwControllerMethod(Id:"23CHs7ehlnO4", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<CrewScheduleStatusLogic>> NewAsync([FromBody]CrewScheduleStatusLogic l)
        {
            return await DoNewAsync<CrewScheduleStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/crewschedulestatu/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "6eKfJrPhPhosl", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<CrewScheduleStatusLogic>> EditAsync([FromRoute] string id, [FromBody]CrewScheduleStatusLogic l)
        {
            return await DoEditAsync<CrewScheduleStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/crewschedulestatus/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"3NiOzPjrgVcd", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CrewScheduleStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
