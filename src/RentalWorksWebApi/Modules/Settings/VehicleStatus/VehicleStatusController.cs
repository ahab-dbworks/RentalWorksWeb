using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.VehicleStatus
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
        [FwControllerMethod(Id:"g5d3m9MhpFGuH")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"qthghboErBBZs")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclestatus
        [HttpGet]
        [FwControllerMethod(Id:"2LpUKmMIUdYAi")]
        public async Task<ActionResult<IEnumerable<VehicleStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VehicleStatusLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclestatus/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"8D0h9eHATDW2w")]
        public async Task<ActionResult<VehicleStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VehicleStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclestatus
        [HttpPost]
        [FwControllerMethod(Id:"hm6lah1DHwRbj")]
        public async Task<ActionResult<VehicleStatusLogic>> PostAsync([FromBody]VehicleStatusLogic l)
        {
            return await DoPostAsync<VehicleStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vehiclestatus/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"Gp565Hg6tcC6Z")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<VehicleStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
