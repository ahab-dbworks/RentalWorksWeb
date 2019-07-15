using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.VehicleColor
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"vxf0Ur4W8UEzw")]
    public class VehicleColorController : AppDataController
    {
        public VehicleColorController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VehicleColorLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclecolor/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"kw0SKPDlNL1Of")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"9ca2tE331LxAw")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclecolor
        [HttpGet]
        [FwControllerMethod(Id:"pjzJe6kNFBnpW")]
        public async Task<ActionResult<IEnumerable<VehicleColorLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VehicleColorLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclecolor/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"FthDulumEG5aG")]
        public async Task<ActionResult<VehicleColorLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VehicleColorLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclecolor
        [HttpPost]
        [FwControllerMethod(Id:"tU2VqNNvIDwx0")]
        public async Task<ActionResult<VehicleColorLogic>> PostAsync([FromBody]VehicleColorLogic l)
        {
            return await DoPostAsync<VehicleColorLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vehiclecolor/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"SnWPkeSWX2T7o")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<VehicleColorLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
