using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace WebApi.Modules.Settings.ContactTitle
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"PClZ3w0VUnPt")]
    public class ContactTitleController : AppDataController
    {
        public ContactTitleController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ContactTitleLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/contacttitle/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"YyKb7wZVkPdW")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"dQQLJ8cxzwbG")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/contacttitle
        [HttpGet]
        [FwControllerMethod(Id:"g4sA2cFUleeL")]
        public async Task<ActionResult<IEnumerable<ContactTitleLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<ContactTitleLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/contacttitle/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"QGGFZ82cpZdR")]
        public async Task<ActionResult<ContactTitleLogic>> GetOneAsync(string id)
        {
            return await DoGetAsync<ContactTitleLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/contacttitle
        [HttpPost]
        [FwControllerMethod(Id:"KQlmrxaeUb6R")]
        public async Task<ActionResult<ContactTitleLogic>> PostAsync([FromBody]ContactTitleLogic l)
        {
            return await DoPostAsync<ContactTitleLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/contacttitle/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"2ovTDXxx0TEI")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync<ContactTitleLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
