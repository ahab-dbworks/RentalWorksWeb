﻿using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.LaborCategory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class LaborCategoryController : AppDataController
    {
        public LaborCategoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(LaborCategoryLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/laborcategory/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(LaborCategoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/laborcategory
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<LaborCategoryLogic>(pageno, pagesize, sort, typeof(LaborCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/laborcategory/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<LaborCategoryLogic>(id, typeof(LaborCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/laborcategory
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]LaborCategoryLogic l)
        {
            return await DoPostAsync<LaborCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/laborcategory/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(LaborCategoryLogic));
        }
        //------------------------------------------------------------------------------------
    }
}