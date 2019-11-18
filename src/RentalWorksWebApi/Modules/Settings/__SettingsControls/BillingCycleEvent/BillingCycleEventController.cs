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
        [FwControllerMethod(Id:"OcZEhvLDQla")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"AO0mFJ3eJXM")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/billingcycleevent
        [HttpGet]
        [FwControllerMethod(Id:"D5GGZ83FMXTn")]
        public async Task<ActionResult<IEnumerable<BillingCycleEventLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<BillingCycleEventLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/billingcycleevent/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"olaPCsOPsbA")]
        public async Task<ActionResult<BillingCycleEventLogic>> GetAsync(string id)
        {
            return await DoGetAsync<BillingCycleEventLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/billingcycleevent
        [HttpPost]
        [FwControllerMethod(Id:"vwBVU8bJcGQ")]
        public async Task<ActionResult<BillingCycleEventLogic>> PostAsync([FromBody]BillingCycleEventLogic l)
        {
            return await DoPostAsync<BillingCycleEventLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/billingcycleevent/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"vV8MCrsWZXU")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync<BillingCycleEventLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
