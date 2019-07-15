using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.DiscountReason
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"XyjAvHBEaKL7")]
    public class DiscountReasonController : AppDataController
    {
        public DiscountReasonController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DiscountReasonLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/discountreason/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"W4UD9aC3FbTQ")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"pMWYAU9CNIaY")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/discountreason
        [HttpGet]
        [FwControllerMethod(Id:"ghvyQxS9Gfyg")]
        public async Task<ActionResult<IEnumerable<DiscountReasonLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DiscountReasonLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/discountreason/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"PAPcb9pRoBJh")]
        public async Task<ActionResult<DiscountReasonLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DiscountReasonLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/discountreason
        [HttpPost]
        [FwControllerMethod(Id:"ezsv35PM7URv")]
        public async Task<ActionResult<DiscountReasonLogic>> PostAsync([FromBody]DiscountReasonLogic l)
        {
            return await DoPostAsync<DiscountReasonLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/discountreason/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"PIrEeU16N38D")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<DiscountReasonLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
