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
        [FwControllerMethod(Id:"paBXIv7DGk371")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"Rs9yIPWM1oKZs")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehicletypewarehouse
        [HttpGet]
        [FwControllerMethod(Id:"E3o6xT9c1qbtn")]
        public async Task<ActionResult<IEnumerable<VehicleTypeWarehouseLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VehicleTypeWarehouseLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehicletypewarehouse/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"3KwawTnIdutMP")]
        public async Task<ActionResult<VehicleTypeWarehouseLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VehicleTypeWarehouseLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehicletypewarehouse
        [HttpPost]
        [FwControllerMethod(Id:"czwsnQS2lUfCw")]
        public async Task<ActionResult<VehicleTypeWarehouseLogic>> PostAsync([FromBody]VehicleTypeWarehouseLogic l)
        {
            return await DoPostAsync<VehicleTypeWarehouseLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vehicletypewarehouse/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"qQXXte1VmO2gp")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<VehicleTypeWarehouseLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
