using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.VehicleFuelType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"T1uhkfrSK7J3d")]
    public class VehicleFuelTypeController : AppDataController
    {
        public VehicleFuelTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VehicleFuelTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclefueltype/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"reYDaNlxVaKkr")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"eOnGLAVCKtqIy")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclefueltype
        [HttpGet]
        [FwControllerMethod(Id:"pVUc6jYVRjEwV")]
        public async Task<ActionResult<IEnumerable<VehicleFuelTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VehicleFuelTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclefueltype/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"PMCqRBUCsGSHd")]
        public async Task<ActionResult<VehicleFuelTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VehicleFuelTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclefueltype
        [HttpPost]
        [FwControllerMethod(Id:"zrzSmn5iLxHWT")]
        public async Task<ActionResult<VehicleFuelTypeLogic>> PostAsync([FromBody]VehicleFuelTypeLogic l)
        {
            return await DoPostAsync<VehicleFuelTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vehiclefueltype/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"YPNimINgYh0Ey")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<VehicleFuelTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
