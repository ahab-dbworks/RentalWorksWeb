﻿using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.GeneratorMake
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class GeneratorMakeController : AppDataController
    {
        public GeneratorMakeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(GeneratorMakeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatormake/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(GeneratorMakeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatormake
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GeneratorMakeLogic>(pageno, pagesize, sort, typeof(GeneratorMakeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatormake/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<GeneratorMakeLogic>(id, typeof(GeneratorMakeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatormake
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]GeneratorMakeLogic l)
        {
            return await DoPostAsync<GeneratorMakeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/generatormake/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(GeneratorMakeLogic));
        }
        //------------------------------------------------------------------------------------
}
}