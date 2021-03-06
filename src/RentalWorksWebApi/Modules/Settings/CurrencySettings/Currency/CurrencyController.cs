using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebApi.Modules.Settings.CurrencySettings.Currency;

namespace WebApi.Modules.Settings.CurrencySettings.Currency
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"xpyZJmmju0uB")]
    public class CurrencyController : AppDataController
    {
        public CurrencyController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CurrencyLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/currency/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"kVgA6o6jUXxb", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            System.Console.WriteLine("Currency Browse");
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"p9bRsqVfH9Ds", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/currency
        [HttpGet]
        [FwControllerMethod(Id:"57JBRqWtA9aq", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<CurrencyLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<CurrencyLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/currency/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"7GnJkw2izlDA", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<CurrencyLogic>> GetOneAsync(string id)
        {
            return await DoGetAsync<CurrencyLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/currency
        [HttpPost]
        [FwControllerMethod(Id:"33zNZpmbNDuH", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<CurrencyLogic>> NewAsync([FromBody]CurrencyLogic l)
        {
            return await DoNewAsync<CurrencyLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/currenc/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "LhywNqMSgV39m", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<CurrencyLogic>> EditAsync([FromRoute] string id, [FromBody]CurrencyLogic l)
        {
            return await DoEditAsync<CurrencyLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/currency/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"fC8wVsWf2bpt", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync<CurrencyLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
