using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.SubCategory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"vHMa0l5PUysXo")]
    public class SubCategoryController : AppDataController
    {
        public SubCategoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SubCategoryLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/subcategory/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"SXbG6Anbjbfya")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"cTj9UaQwZutRw")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/subcategory
        [HttpGet]
        [FwControllerMethod(Id:"RJqmzcSQKDakA")]
        public async Task<ActionResult<IEnumerable<SubCategoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SubCategoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/subcategory/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"9leA68QQUlABO")]
        public async Task<ActionResult<SubCategoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SubCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/subcategory
        [HttpPost]
        [FwControllerMethod(Id:"y0L69N4AOKpl8")]
        public async Task<ActionResult<SubCategoryLogic>> PostAsync([FromBody]SubCategoryLogic l)
        {
            return await DoPostAsync<SubCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/subcategory/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"8uXzlXqS43vGI")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<SubCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
