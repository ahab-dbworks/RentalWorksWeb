using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.VehicleSettings.VehicleMake
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"Kacj9CuAA7F8m")]
    public class VehicleMakeController : AppDataController
    {
        public VehicleMakeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VehicleMakeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclemake/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"4hPy7bQ5rrbuJ", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"BIisaj2hDzaMj", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclemake
        [HttpGet]
        [FwControllerMethod(Id:"6qDWE4uVJRoeu", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<VehicleMakeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VehicleMakeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclemake/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"isxa8XZr6TBO3", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<VehicleMakeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VehicleMakeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclemake
        [HttpPost]
        [FwControllerMethod(Id:"gwMgg12xd9XC2", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<VehicleMakeLogic>> NewAsync([FromBody]VehicleMakeLogic l)
        {
            return await DoNewAsync<VehicleMakeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/vehiclemak/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "cDCvkg02Fz0ZF", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<VehicleMakeLogic>> EditAsync([FromRoute] string id, [FromBody]VehicleMakeLogic l)
        {
            return await DoEditAsync<VehicleMakeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vehiclemake/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"pXWzF8apehVM4", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<VehicleMakeLogic>(id);
        }
        //------------------------------------------------------------------------------------
}
}
