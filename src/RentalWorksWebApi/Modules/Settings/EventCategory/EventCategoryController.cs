using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Modules.Settings.EventCategory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"0Zcc827UeucP")]
    public class EventCategoryController : AppDataController
    {
        public EventCategoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(EventCategoryLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/eventcategory/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"0rcuAndrKkyS")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"dhVCOgPphPQH")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/eventcategory
        [HttpGet]
        [FwControllerMethod(Id:"1L3jI2UMPfPD")]
        public async Task<ActionResult<IEnumerable<EventCategoryLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<EventCategoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/eventcategory/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"eGbk2XuxyPDh")]
        public async Task<ActionResult<EventCategoryLogic>> GetOneAsync(string id)
        {
            return await DoGetAsync<EventCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/eventcategory
        [HttpPost]
        [FwControllerMethod(Id:"F2JDPwqxTNW0")]
        public async Task<ActionResult<EventCategoryLogic>> PostAsync([FromBody]EventCategoryLogic l)
        {
            return await DoPostAsync<EventCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/eventcategory/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"FhOCF54yf6fS")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync<EventCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
