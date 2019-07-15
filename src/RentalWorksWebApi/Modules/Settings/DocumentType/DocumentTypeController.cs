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
        [FwControllerMethod(Id:"2KyXXsmQqtI1")]
        public async Task<ActionResult<IEnumerable<DocumentTypeLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<DocumentTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/documenttype/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"AzIIz1Voc5Kx")]
        public async Task<ActionResult<DocumentTypeLogic>> GetAsync(string id)
        {
            return await DoGetAsync<DocumentTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/documenttype
        [HttpPost]
        [FwControllerMethod(Id:"4GWLUfRi90in")]
        public async Task<ActionResult<DocumentTypeLogic>> PostAsync([FromBody]DocumentTypeLogic l)
        {
            return await DoPostAsync<DocumentTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/documenttype/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"D53ZIX4E6pMx")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync<DocumentTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
