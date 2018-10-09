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
    public class FacilityTypeController : AppDataController
    {
        public FacilityTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(FacilityTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilitytype/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilitytype
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacilityTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FacilityTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilitytype/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<FacilityTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<FacilityTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilitytype
        [HttpPost]
        public async Task<ActionResult<FacilityTypeLogic>> PostAsync([FromBody]FacilityTypeLogic l)
        {
            return await DoPostAsync<FacilityTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/facilitytype/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
    }
}