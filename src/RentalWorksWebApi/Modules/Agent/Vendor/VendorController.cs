using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Modules.Agent.Vendor
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"cwytGLEcUzJdn")]
    public class VendorController : AppDataController
    {
        public VendorController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VendorLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendor/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"qISJ4BW5wxoWa")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"ZUpDxBZVU2ahJ")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendor
        [HttpGet]
        [FwControllerMethod(Id:"AIdpqgM5qd")]
        public async Task<ActionResult<IEnumerable<VendorLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<VendorLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendor/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"vkY3kGToPg")]
        public async Task<ActionResult<VendorLogic>> GetOneAsync(string id)
        {
            return await DoGetAsync<VendorLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendor
        [HttpPost]
        [FwControllerMethod(Id:"PNyHqNlPW2crp")]
        public async Task<ActionResult<VendorLogic>> PostAsync([FromBody]VendorLogic l)
        {
            return await DoPostAsync<VendorLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vendor/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"5iVmNJZGN9LDk")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync<VendorLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
