﻿using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System;
using System.Threading.Tasks;

namespace WebApi.Modules.Home.CustomerNote
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class CustomerNoteController : AppDataController
    {
        public CustomerNoteController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CustomerNoteLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/customernote/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(CustomerNoteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customernote
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomerNoteLogic>(pageno, pagesize, sort, typeof(CustomerNoteLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customernote/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CustomerNoteLogic>(id, typeof(CustomerNoteLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customernote
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]CustomerNoteLogic l)
        {
            return await DoPostAsync<CustomerNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customernote/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(CustomerNoteLogic));
        }
        //------------------------------------------------------------------------------------
    }
}