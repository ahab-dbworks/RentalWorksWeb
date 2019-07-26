using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.Category
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id: "pWsHOgp1o7Obw")]
    public class CategoryController : AppDataController
    {
        public CategoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CategoryLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/category/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id: "qBtRVUMsxST2W")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/category/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "P6LxcR2INhGe4")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        //// GET api/v1/category
        //[HttpGet]
        //[FwControllerMethod(Id: "xxxxxxxxxxxx")]
        //public async Task<ActionResult<IEnumerable<CategoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        //{
        //    return await DoGetAsync<CategoryLogic>(pageno, pagesize, sort);
        //}
        ////------------------------------------------------------------------------------------
        //// GET api/v1/category/A0000001
        //[HttpGet("{id}")]
        //[FwControllerMethod(Id: "xxxxxxxxxxxx")]
        //public async Task<ActionResult<CategoryLogic>> GetOneAsync([FromRoute]string id)
        //{
        //    return await DoGetAsync<CategoryLogic>(id);
        //}
        ////------------------------------------------------------------------------------------
        //// POST api/v1/category
        //[HttpPost]
        //[FwControllerMethod(Id: "xxxxxxxxxxxx")]
        //public async Task<ActionResult<CategoryLogic>> PostAsync([FromBody]CategoryLogic l)
        //{
        //    return await DoPostAsync<CategoryLogic>(l);
        //}
        ////------------------------------------------------------------------------------------
        //// DELETE api/v1/category/A0000001
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id:"FDQGC9RXy6eKi")]
        //public async Task<ActionResult<"xxxxxxxxxxxx">> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync<CategoryLogic>(id);
        //}
        ////------------------------------------------------------------------------------------
    }
}
