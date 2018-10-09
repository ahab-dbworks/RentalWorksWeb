﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.WardrobeGender
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class WardrobeGenderController : AppDataController
    {
        public WardrobeGenderController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WardrobeGenderLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobegender/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobegender
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WardrobeGenderLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WardrobeGenderLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobegender/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<WardrobeGenderLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WardrobeGenderLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobegender
        [HttpPost]
        public async Task<ActionResult<WardrobeGenderLogic>> PostAsync([FromBody]WardrobeGenderLogic l)
        {
            return await DoPostAsync<WardrobeGenderLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/wardrobegender/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
    }
}