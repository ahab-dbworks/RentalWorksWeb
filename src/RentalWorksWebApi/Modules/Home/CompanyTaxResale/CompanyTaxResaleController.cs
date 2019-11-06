using FwStandard.AppManager;
﻿using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Home.CompanyTaxResale
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"k48X9sulRpmb")]
    public class CompanyTaxResaleController : AppDataController
    {
        public CompanyTaxResaleController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CompanyTaxResaleLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/companytaxresale/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"SQnfqL13U06B")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"SQnfqL13U06B")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/companytaxresale
        [HttpGet]
        [FwControllerMethod(Id:"oGmuyYCFo3qh")]
        public async Task<ActionResult<IEnumerable<CompanyTaxResaleLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CompanyTaxResaleLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/companytaxresale/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"3BG5Ffpc5wjc")]
        public async Task<ActionResult<CompanyTaxResaleLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CompanyTaxResaleLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/companytaxresale
        [HttpPost]
        [FwControllerMethod(Id:"NmwcQmlnDHv1")]
        public async Task<ActionResult<CompanyTaxResaleLogic>> PostAsync([FromBody]CompanyTaxResaleLogic l)
        {
            return await DoPostAsync<CompanyTaxResaleLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/companytaxresale/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"uA5m7CZ6VGc6")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CompanyTaxResaleLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
