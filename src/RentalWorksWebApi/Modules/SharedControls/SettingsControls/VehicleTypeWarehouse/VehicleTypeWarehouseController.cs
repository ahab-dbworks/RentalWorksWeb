using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.VehicleTypeWarehouse
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"5Oz300mlivVCc")]
    public class VehicleTypeWarehouseController : AppDataController
    {
        public VehicleTypeWarehouseController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VehicleTypeWarehouseLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehicletypewarehouse/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"paBXIv7DGk371", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"Rs9yIPWM1oKZs", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehicletypewarehouse
        [HttpGet]
        [FwControllerMethod(Id:"E3o6xT9c1qbtn", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<VehicleTypeWarehouseLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VehicleTypeWarehouseLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehicletypewarehouse/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"3KwawTnIdutMP", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<VehicleTypeWarehouseLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VehicleTypeWarehouseLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehicletypewarehouse
        [HttpPost]
        [FwControllerMethod(Id:"czwsnQS2lUfCw", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<VehicleTypeWarehouseLogic>> NewAsync([FromBody]VehicleTypeWarehouseLogic l)
        {
            return await DoNewAsync<VehicleTypeWarehouseLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/vehicletypewarehous/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "SZM5lTOCB5UPC", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<VehicleTypeWarehouseLogic>> EditAsync([FromRoute] string id, [FromBody]VehicleTypeWarehouseLogic l)
        {
            return await DoEditAsync<VehicleTypeWarehouseLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vehicletypewarehouse/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"qQXXte1VmO2gp", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<VehicleTypeWarehouseLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
