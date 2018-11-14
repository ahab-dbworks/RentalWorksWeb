using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Settings.TaxOption
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"gYT7BJnFn9SLc")]
    public class TaxOptionController : AppDataController
    {
        public TaxOptionController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(TaxOptionLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/taxoption/browse
        [HttpPost("browse")]
        [Authorize(Policy = "{051A9D01-5B55-4805-931A-75937FA04F33}")]
        [FwControllerMethod(Id:"8n3uk1wkv7Aty")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"W00ccjH8tXymw")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/taxoption
        [HttpGet]
        [Authorize(Policy = "{A17E8E69-1427-472E-9F15-EA4E31590ABE}")]
        [FwControllerMethod(Id:"HVUYo1siIIJDq")]
        public async Task<ActionResult<IEnumerable<TaxOptionLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<TaxOptionLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/taxoption/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "{A8FFA28F-E260-4B28-BB88-B4A5C2F0745B}")]
        [FwControllerMethod(Id:"uGhPTfsN06ODS")]
        public async Task<ActionResult<TaxOptionLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<TaxOptionLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/taxoption
        [HttpPost]
        [Authorize(Policy = "{C0DAAFEF-B169-4153-A6D4-C3B6D3C07F94}")]
        [FwControllerMethod(Id:"C7hnOuTsEeLr1")]
        public async Task<ActionResult<TaxOptionLogic>> PostAsync([FromBody]TaxOptionLogic l)
        {
            return await DoPostAsync<TaxOptionLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/taxoption/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{0C9DE590-155F-459E-B33E-90AEABFB5F50}")]
        [FwControllerMethod(Id:"lE3Ob6a7tDCKi")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
        // POST api/v1/taxoption/A0000001/forcerates
        [HttpPost("{id}/forcerates")]
        [FwControllerMethod(Id:"lfZiNgs8GOJBE")]
        public async Task<ActionResult<bool>> ForceRatesAsync([FromRoute]string id)
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
    }
}
