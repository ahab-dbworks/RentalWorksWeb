using FwStandard.AppManager;
﻿using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;

namespace WebApi.Modules.Settings.DealSettings.ProductionType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"3UvqzQ0Svxay6")]
    public class ProductionTypeController : AppDataController
    {
        public ProductionTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ProductionTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/productiontype/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"JIcxN44tLlofQ")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"JIcxN44tLlofQ")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/productiontype
        [HttpGet]
        [FwControllerMethod(Id:"8xF9v8K5JxwwX")]
        public async Task<ActionResult<IEnumerable<ProductionTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProductionTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/productiontype/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"mPZdwJkHjd0vW")]
        public async Task<ActionResult<ProductionTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProductionTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/productiontype
        [HttpPost]
        [FwControllerMethod(Id:"IDColiml86J2I")]
        public async Task<ActionResult<ProductionTypeLogic>> PostAsync([FromBody]ProductionTypeLogic l)
        {
            return await DoPostAsync<ProductionTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/productiontype/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"YEB6Xojl8VoEU")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ProductionTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
