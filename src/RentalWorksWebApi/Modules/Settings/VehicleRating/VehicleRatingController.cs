using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.VehicleRating
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"8VAXVhw2HMt53")]
    public class VehicleRatingController : AppDataController
    {
        public VehicleRatingController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VehicleRatingLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclerating/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"1jNLzdKZSsr3S")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"Wz1esXPv40EYf")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclerating
        [HttpGet]
        [FwControllerMethod(Id:"aQMEDqVvHj7AY")]
        public async Task<ActionResult<IEnumerable<VehicleRatingLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VehicleRatingLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclerating/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"SxST66h4lao8G")]
        public async Task<ActionResult<VehicleRatingLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VehicleRatingLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclerating
        [HttpPost]
        [FwControllerMethod(Id:"P2cMOsrkrlvBC")]
        public async Task<ActionResult<VehicleRatingLogic>> PostAsync([FromBody]VehicleRatingLogic l)
        {
            return await DoPostAsync<VehicleRatingLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vehiclerating/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"F53WraMD2cOld")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<VehicleRatingLogic>(id);
        }
        //------------------------------------------------------------------------------------
}
}
