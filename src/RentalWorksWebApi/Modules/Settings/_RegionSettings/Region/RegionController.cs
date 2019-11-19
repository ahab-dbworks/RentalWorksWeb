using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Modules.Settings.RegionSettings.Region
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"pqSlzQGRVmxiE")]
    public class RegionController : AppDataController
    {
        public RegionController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RegionLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/region/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"bsNvNjCV5utAV")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"nsCsCZqKGUFS0")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/region
        [HttpGet]
        [FwControllerMethod(Id:"9oK4RFbswB")]
        public async Task<ActionResult<IEnumerable<RegionLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<RegionLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/region/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"uBaSNthlTnXun")]
        public async Task<ActionResult<RegionLogic>> GetAsync(string id)
        {
            return await DoGetAsync<RegionLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/region
        [HttpPost]
        [FwControllerMethod(Id:"G2csjVAM8rYnj")]
        public async Task<ActionResult<RegionLogic>> PostAsync([FromBody]RegionLogic l)
        {
            return await DoPostAsync<RegionLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/region/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"y6aHyItQJkPWo")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync<RegionLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
