using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Modules.Home.CompanyTaxOption
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"B9CzDEmYe1Zf")]
    public class CompanyTaxOptionController : AppDataController
    {
        public CompanyTaxOptionController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CompanyTaxOptionLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/companytaxoption/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"zFk5XqrpU9wL")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"CHmn2Nz0QcLp")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/companytaxoption
        [HttpGet]
        [FwControllerMethod(Id:"7FWfsyD36AIJ")]
        public async Task<ActionResult<IEnumerable<CompanyTaxOptionLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<CompanyTaxOptionLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/companytaxoption/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"ruPmZgIcLg4a")]
        public async Task<ActionResult<CompanyTaxOptionLogic>> GetOneAsync(string id)
        {
            return await DoGetAsync<CompanyTaxOptionLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/companytaxoption
        [HttpPost]
        [FwControllerMethod(Id:"IKitIaPutFUf")]
        public async Task<ActionResult<CompanyTaxOptionLogic>> PostAsync([FromBody]CompanyTaxOptionLogic l)
        {
            // NOTE: insert is currently not supported becase there is no surrogate key in this module
            return await DoPostAsync<CompanyTaxOptionLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/companytaxoption/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"qYpDSTRcigYE")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync<CompanyTaxOptionLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
