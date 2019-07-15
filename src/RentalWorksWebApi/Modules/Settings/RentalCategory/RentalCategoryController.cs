using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.RentalCategory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"whxFImy6IZG2p")]
    public class RentalCategoryController : AppDataController
    {
        public RentalCategoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RentalCategoryLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/rentalcategory/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"f8RNwGI6wXDyf")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"SFOKmae3MqpMJ")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/rentalcategory
        [HttpGet]
        [FwControllerMethod(Id:"r8i1dPqDT8ox9")]
        public async Task<ActionResult<IEnumerable<RentalCategoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RentalCategoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/rentalcategory/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"anPAUbeBwi8f0")]
        public async Task<ActionResult<RentalCategoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RentalCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/rentalcategory
        [HttpPost]
        [FwControllerMethod(Id:"eKNlff3tW3swN")]
        public async Task<ActionResult<RentalCategoryLogic>> PostAsync([FromBody]RentalCategoryLogic l)
        {
            return await DoPostAsync<RentalCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/rentalcategory/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"FDQGC9RXy6eKi")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<RentalCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
