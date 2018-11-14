using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.DealType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"jZCS1X5BzeyS")]
    public class DealTypeController : AppDataController
    {
        public DealTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DealTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealtype/browse
        [HttpPost("browse")]
        [Authorize(Policy = "{9B31A9CF-F852-45F4-9944-4AE386C826C7}")]
        [FwControllerMethod(Id:"T2C76lYORyv1")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"janBTZ7FJUdx")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/dealtype
        [HttpGet]
        [Authorize(Policy = "{9862D27F-0B5C-4399-A238-DD306EC7C39C}")]
        [FwControllerMethod(Id:"A1yFIADuIAMc")]
        public async Task<ActionResult<IEnumerable<DealTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DealTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/dealtype/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "{7B6498F1-EC58-4627-9E71-67C689FB37A8}")]
        [FwControllerMethod(Id:"BVncSxXlNj4Z")]
        public async Task<ActionResult<DealTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DealTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealtype
        [HttpPost]
        [Authorize(Policy = "{044DC0AB-B54A-4A29-A784-648E341BDC06}")]
        [FwControllerMethod(Id:"GKDbyDl9TPLm")]
        public async Task<ActionResult<DealTypeLogic>> PostAsync([FromBody]DealTypeLogic l)
        {
            return await DoPostAsync<DealTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/dealtype/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{B9BFE8F0-BB31-40A3-9DAC-A38C4CA65F30}")]
        [FwControllerMethod(Id:"gEidHhuH1Fx6")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
    }
}
