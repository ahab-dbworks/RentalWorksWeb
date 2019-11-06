using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Home.DealNote
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"jcwmVLFEU88k")]
    public class DealNoteController : AppDataController
    {
        public DealNoteController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DealNoteLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealnote/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"6gsPqyJww0xO")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"p0qqo7jrqCwR")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/dealnote
        [HttpGet]
        [FwControllerMethod(Id:"MLijscwOz2fi")]
        public async Task<ActionResult<IEnumerable<DealNoteLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DealNoteLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/dealnote/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"1gHJNuI6E7Rc")]
        public async Task<ActionResult<DealNoteLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DealNoteLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealnote
        [HttpPost]
        [FwControllerMethod(Id:"DXnjLoOeRIyh")]
        public async Task<ActionResult<DealNoteLogic>> PostAsync([FromBody]DealNoteLogic l)
        {
            return await DoPostAsync<DealNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/dealnote/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"d1OFeEn5KNCi")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<DealNoteLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
