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
    public class DiscountReasonController : AppDataController
    {
        public DiscountReasonController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DiscountReasonLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/discountreason/browse
        [HttpPost("browse")]
        [Authorize(Policy = "")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(DiscountReasonLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/discountreason
        [HttpGet]
        [Authorize(Policy = "")]
        public async Task<ActionResult<IEnumerable<DiscountReasonLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DiscountReasonLogic>(pageno, pagesize, sort, typeof(DiscountReasonLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/discountreason/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "")]
        public async Task<ActionResult<DiscountReasonLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DiscountReasonLogic>(id, typeof(DiscountReasonLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/discountreason
        [HttpPost]
        [Authorize(Policy = "")]
        public async Task<ActionResult<DiscountReasonLogic>> PostAsync([FromBody]DiscountReasonLogic l)
        {
            return await DoPostAsync<DiscountReasonLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/discountreason/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(DiscountReasonLogic));
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}