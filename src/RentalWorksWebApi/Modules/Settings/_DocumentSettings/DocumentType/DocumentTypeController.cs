using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Modules.Settings.DocumentSettings.DocumentType
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
        [FwControllerMethod(Id:"peQXVZYW7oXi", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"lVQW2HvK44cI", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/documenttype
        [HttpGet]
        [FwControllerMethod(Id:"2KyXXsmQqtI1", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<DocumentTypeLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<DocumentTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/documenttype/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"AzIIz1Voc5Kx", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<DocumentTypeLogic>> GetOneAsync(string id)
        {
            return await DoGetAsync<DocumentTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/documenttype
        [HttpPost]
        [FwControllerMethod(Id:"4GWLUfRi90in", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<DocumentTypeLogic>> NewAsync([FromBody]DocumentTypeLogic l)
        {
            return await DoNewAsync<DocumentTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/documenttyp/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "XHAVBgOP4zpFi", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<DocumentTypeLogic>> EditAsync([FromRoute] string id, [FromBody]DocumentTypeLogic l)
        {
            return await DoEditAsync<DocumentTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/documenttype/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"D53ZIX4E6pMx", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync<DocumentTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
