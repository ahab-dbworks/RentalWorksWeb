using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace WebApi.Modules.Settings.BillingCycleEvent
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"KSA8EsXjcrt")]
    public class BillingCycleEventController : AppDataController
    {
        public BillingCycleEventController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(BillingCycleEventLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/billingcycleevent/browse
        [HttpPost("browse")]
        [Authorize(Policy = "{5973FA5B-5519-45DC-9ABF-EF6AF65471C1}")]
        [FwControllerMethod(Id:"OcZEhvLDQla")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"AO0mFJ3eJXM")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/billingcycleevent
        [HttpGet]
        [Authorize(Policy = "{1415227E-1A40-4492-B519-462EC788CDE1}")]
        [FwControllerMethod(Id:"D5GGZ83FMXTn")]
        public async Task<ActionResult<IEnumerable<BillingCycleEventLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<BillingCycleEventLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/billingcycleevent/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "{15A4DD14-CE3C-454E-B475-62B6BE30081F}")]
        [FwControllerMethod(Id:"olaPCsOPsbA")]
        public async Task<ActionResult<BillingCycleEventLogic>> GetAsync(string id)
        {
            return await DoGetAsync<BillingCycleEventLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/billingcycleevent
        [HttpPost]
        [Authorize(Policy = "{35C46276-BBB7-43ED-BBE6-98FFB12655DC}")]
        [FwControllerMethod(Id:"vwBVU8bJcGQ")]
        public async Task<ActionResult<BillingCycleEventLogic>> PostAsync([FromBody]BillingCycleEventLogic l)
        {
            return await DoPostAsync<BillingCycleEventLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/billingcycleevent/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{A2EC754D-B1AB-4355-8803-30DF1D42B49D}")]
        [FwControllerMethod(Id:"vV8MCrsWZXU")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
