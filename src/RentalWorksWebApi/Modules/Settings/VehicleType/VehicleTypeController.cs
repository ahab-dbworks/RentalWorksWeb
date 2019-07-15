using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.VehicleType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"lbbSCJTjhBL3U")]
    public class VehicleTypeController : AppDataController
    {
        public VehicleTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VehicleTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehicletype/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"hllL40yct9uS7")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"B9q87tZkPDNjd")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehicletype
        [HttpGet]
        [FwControllerMethod(Id:"CHjCU7JTKCkPV")]
        public async Task<ActionResult<IEnumerable<VehicleTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VehicleTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehicletype/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"yRdcEf24Uj8yE")]
        public async Task<ActionResult<VehicleTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VehicleTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehicletype
        [HttpPost]
        [FwControllerMethod(Id:"zV2HP24MJuC1E")]
        public async Task<ActionResult<VehicleTypeLogic>> PostAsync([FromBody]VehicleTypeLogic l)
        {
            return await DoPostAsync<VehicleTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vehicletype/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"4srmkJomyCHgA")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<VehicleTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
