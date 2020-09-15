using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.Settings.AccountingSettings.GlAccount;

namespace WebApi.Modules.Settings.TaxSettings.TaxOption
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id: "gYT7BJnFn9SLc")]
    public class TaxOptionController : AppDataController
    {
        public TaxOptionController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(TaxOptionLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/taxoption/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id: "8n3uk1wkv7Aty", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "W00ccjH8tXymw", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/taxoption
        [HttpGet]
        [FwControllerMethod(Id: "HVUYo1siIIJDq", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<TaxOptionLogic>>> GetManyAsync([FromQuery] int pageno, [FromQuery] int pagesize, [FromQuery] string sort)
        {
            return await DoGetAsync<TaxOptionLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/taxoption/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "uGhPTfsN06ODS", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<TaxOptionLogic>> GetOneAsync([FromRoute] string id)
        {
            return await DoGetAsync<TaxOptionLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/taxoption
        [HttpPost]
        [FwControllerMethod(Id: "C7hnOuTsEeLr1", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<TaxOptionLogic>> NewAsync([FromBody] TaxOptionLogic l)
        {
            return await DoNewAsync<TaxOptionLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/taxoptio/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "wsCyHjJAgTZdS", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<TaxOptionLogic>> EditAsync([FromRoute] string id, [FromBody] TaxOptionLogic l)
        {
            return await DoEditAsync<TaxOptionLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/taxoption/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "lE3Ob6a7tDCKi", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute] string id)
        {
            return await DoDeleteAsync<TaxOptionLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
        // POST api/v1/taxoption/A0000001/forcerates
        [HttpPost("{id}/forcerates")]
        [FwControllerMethod(Id: "lfZiNgs8GOJBE", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<bool>> ForceRatesAsync([FromRoute] string id)
        {
            try
            {
                string[] ids = id.Split('~');
                TaxOptionLogic l = new TaxOptionLogic();
                l.AppConfig = this.AppConfig;
                bool success = await l.LoadAsync<TaxOptionLogic>(ids);
                if (success)
                {
                    success = l.ForceRates();
                }
                return new OkObjectResult(success);
            }
            catch (Exception ex)
            {
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/taxoption/validatedetaxaccount1/browse
        [HttpPost("validatedetaxaccount1/browse")]
        [FwControllerMethod(Id: "5q54D8p3PFVO", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateTaxAccount1BrowseAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/taxoption/validatetaxaccount2/browse
        [HttpPost("validatetaxaccount2/browse")]
        [FwControllerMethod(Id: "M98bjCXWJrn5", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateTaxAccount2BrowseAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/taxoption/validatedetaxontaxaccount/browse
        [HttpPost("validatedetaxontaxaccount/browse")]
        [FwControllerMethod(Id: "mnv8lD4Hj6sq", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateTaxOnTaxAccountBrowseAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
    }
}
