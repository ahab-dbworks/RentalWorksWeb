using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.VehicleSettings.VehicleStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"RSXpDaDwDtgH3")]
    public class VehicleStatusController : AppDataController
    {
        public VehicleStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VehicleStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclestatus/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"g5d3m9MhpFGuH", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"qthghboErBBZs", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclestatus
        [HttpGet]
        [FwControllerMethod(Id:"2LpUKmMIUdYAi", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<VehicleStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VehicleStatusLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclestatus/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"8D0h9eHATDW2w", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<VehicleStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VehicleStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclestatus
        [HttpPost]
        [FwControllerMethod(Id:"hm6lah1DHwRbj", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<VehicleStatusLogic>> NewAsync([FromBody]VehicleStatusLogic l)
        {
            return await DoNewAsync<VehicleStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/vehiclestatu/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "IyG9K8Y3bjxqS", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<VehicleStatusLogic>> EditAsync([FromRoute] string id, [FromBody]VehicleStatusLogic l)
        {
            return await DoEditAsync<VehicleStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vehiclestatus/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"Gp565Hg6tcC6Z", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<VehicleStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
