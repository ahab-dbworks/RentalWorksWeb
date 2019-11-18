using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.VehicleModel
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"kPPx0KctQjlXx")]
    public class VehicleModelController : AppDataController
    {
        public VehicleModelController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VehicleModelLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclemodel/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"b8ZFJlKVfEvQs")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"eZXq42fUT7nlJ")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclemodel
        [HttpGet]
        [FwControllerMethod(Id:"b7s1FoeORNKe4")]
        public async Task<ActionResult<IEnumerable<VehicleModelLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VehicleModelLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclemodel/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"z12M6s7Rp83Jh")]
        public async Task<ActionResult<VehicleModelLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VehicleModelLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclemodel
        [HttpPost]
        [FwControllerMethod(Id:"6QPJQpAFv1be9")]
        public async Task<ActionResult<VehicleModelLogic>> PostAsync([FromBody]VehicleModelLogic l)
        {
            return await DoPostAsync<VehicleModelLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vehiclemodel/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"58PucxuwjcXOZ")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<VehicleModelLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
