using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.VehicleSettings.VehicleScheduleStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"nvZvZeHdxviKG")]
    public class VehicleScheduleStatusController : AppDataController
    {
        public VehicleScheduleStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VehicleScheduleStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehicleschedulestatus/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"8oGhVG950S4dS", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"ZN4LczLwq6Ejm", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehicleschedulestatus
        [HttpGet]
        [FwControllerMethod(Id:"bdN0kKY2SAl64", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<VehicleScheduleStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VehicleScheduleStatusLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehicleschedulestatus/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"yIV7SiN9vbl94", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<VehicleScheduleStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VehicleScheduleStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehicleschedulestatus
        [HttpPost]
        [FwControllerMethod(Id:"bhQc6x3eX9BYQ", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<VehicleScheduleStatusLogic>> NewAsync([FromBody]VehicleScheduleStatusLogic l)
        {
            return await DoNewAsync<VehicleScheduleStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/vehicleschedulestatu/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "KSWsN2asHQ444", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<VehicleScheduleStatusLogic>> EditAsync([FromRoute] string id, [FromBody]VehicleScheduleStatusLogic l)
        {
            return await DoEditAsync<VehicleScheduleStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vehicleschedulestatus/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"XAnqUkYZWRBBW", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<VehicleScheduleStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
