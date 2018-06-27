﻿using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.RentalCategory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class RentalCategoryController : AppDataController
    {
        public RentalCategoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RentalCategoryLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/rentalcategory/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(RentalCategoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/rentalcategory
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RentalCategoryLogic>(pageno, pagesize, sort, typeof(RentalCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/rentalcategory/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RentalCategoryLogic>(id, typeof(RentalCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/rentalcategory
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]RentalCategoryLogic l)
        {
            return await DoPostAsync<RentalCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/rentalcategory/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(RentalCategoryLogic));
        }
        //------------------------------------------------------------------------------------
    }
}