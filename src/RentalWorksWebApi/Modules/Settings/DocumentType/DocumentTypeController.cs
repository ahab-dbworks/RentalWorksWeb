using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Modules.Settings.DocumentType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"qus92U5z7R9Z")]
    public class DocumentTypeController : AppDataController
    {
        public DocumentTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DocumentTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/documenttype/browse
        [HttpPost("browse")]
        [Authorize(Policy = "{B1C7EC8C-A38B-4B88-A635-77106140632D}")]
        [FwControllerMethod(Id:"peQXVZYW7oXi")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"lVQW2HvK44cI")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/documenttype
        [HttpGet]
        [Authorize(Policy = "{C819F03A-4EE9-482C-8157-B2477DA26DBF}")]
        [FwControllerMethod(Id:"2KyXXsmQqtI1")]
        public async Task<ActionResult<IEnumerable<DocumentTypeLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<DocumentTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/documenttype/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "{64BC04C7-CE73-4124-A138-C51FC5BB064C}")]
        [FwControllerMethod(Id:"AzIIz1Voc5Kx")]
        public async Task<ActionResult<DocumentTypeLogic>> GetAsync(string id)
        {
            return await DoGetAsync<DocumentTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/documenttype
        [HttpPost]
        [Authorize(Policy = "{B13DD731-3955-4FA5-AD32-EE92994CAF7B}")]
        [FwControllerMethod(Id:"4GWLUfRi90in")]
        public async Task<ActionResult<DocumentTypeLogic>> PostAsync([FromBody]DocumentTypeLogic l)
        {
            return await DoPostAsync<DocumentTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/documenttype/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{7EA7F5A5-CD3E-40E8-88B5-74690E33F0D8}")]
        [FwControllerMethod(Id:"D53ZIX4E6pMx")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
