﻿using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.PhotographyType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class PhotographyTypeController : AppDataController
    {
        public PhotographyTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PhotographyTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/photographytype/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PhotographyTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/photographytype
        [HttpGet]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<PhotographyTypeLogic>(pageno, pagesize, sort, typeof(PhotographyTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/photographytype/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return await DoGetAsync<PhotographyTypeLogic>(id, typeof(PhotographyTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/photographytype
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]PhotographyTypeLogic l)
        {
            return await DoPostAsync<PhotographyTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/photographytype/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(PhotographyTypeLogic));
        }
        //------------------------------------------------------------------------------------
    }
}