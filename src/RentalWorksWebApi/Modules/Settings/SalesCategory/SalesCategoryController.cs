using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.SalesCategory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"XS6vdtV5jQTyF")]
    public class SalesCategoryController : AppDataController
    {
        public SalesCategoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SalesCategoryLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/salescategory/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"iGvCDBc6iCTOs")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"7TQBqR8a0d6nU")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/salescategory
        [HttpGet]
        [FwControllerMethod(Id:"nKzzlH2X6e461")]
        public async Task<ActionResult<IEnumerable<SalesCategoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SalesCategoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/salescategory/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"CtaBWOgfnFdio")]
        public async Task<ActionResult<SalesCategoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SalesCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/salescategory
        [HttpPost]
        [FwControllerMethod(Id:"eQE7mrt1VxdOK")]
        public async Task<ActionResult<SalesCategoryLogic>> PostAsync([FromBody]SalesCategoryLogic l)
        {
            return await DoPostAsync<SalesCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/salescategory/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"iXj9iGvriNmmc")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<SalesCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
