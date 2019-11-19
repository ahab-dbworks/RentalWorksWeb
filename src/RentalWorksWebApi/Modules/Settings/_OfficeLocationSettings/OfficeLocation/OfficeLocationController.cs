using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"yZhqRrXdTEvN")]
    public class OfficeLocationController : AppDataController
    {
        public OfficeLocationController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OfficeLocationLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/Location/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"ciKUpRcMkWvL")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"X5zKPFTQcXFh")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/Location
        [HttpGet]
        [FwControllerMethod(Id:"2Q8uVuiEvufJ")]
        public async Task<ActionResult<IEnumerable<OfficeLocationLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OfficeLocationLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/Location/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"WvFQsN6sMTXV")]
        public async Task<ActionResult<OfficeLocationLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OfficeLocationLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/Location
        [HttpPost]
        [FwControllerMethod(Id:"cT02J0bWYX6v")]
        public async Task<ActionResult<OfficeLocationLogic>> PostAsync([FromBody]OfficeLocationLogic l)
        {
            return await DoPostAsync<OfficeLocationLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/Location/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"YvPKiTjiJyO9")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<OfficeLocationLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
