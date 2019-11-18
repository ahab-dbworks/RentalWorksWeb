using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.FacilityType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"sp3q4geu1RZM")]
    public class FacilityTypeController : AppDataController
    {
        public FacilityTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(FacilityTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilitytype/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"ndr2gAHvQjB3")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"WRSxPZb0v5iO")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilitytype
        [HttpGet]
        [FwControllerMethod(Id:"KwEATUghLmiv")]
        public async Task<ActionResult<IEnumerable<FacilityTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FacilityTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilitytype/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"j9I6mtDok1MS")]
        public async Task<ActionResult<FacilityTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<FacilityTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilitytype
        [HttpPost]
        [FwControllerMethod(Id:"Oi2ax1ON7lOI")]
        public async Task<ActionResult<FacilityTypeLogic>> PostAsync([FromBody]FacilityTypeLogic l)
        {
            return await DoPostAsync<FacilityTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/facilitytype/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"vSVG7sWVZhiK")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<FacilityTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
