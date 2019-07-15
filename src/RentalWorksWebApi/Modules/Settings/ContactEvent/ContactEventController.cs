using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Modules.Settings.ContactEvent
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"KdvGpc1dQINo")]
    public class ContactEventController : AppDataController
    {
        public ContactEventController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ContactEventLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/contactevent/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"2ceG6kfzUXUl")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"NqaCPC2VgbVI")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/contactevent
        [HttpGet]
        [FwControllerMethod(Id:"aEaKP88yM6UE")]
        public async Task<ActionResult<IEnumerable<ContactEventLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<ContactEventLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/contactevent/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"QyS0jK9Pyt3L")]
        public async Task<ActionResult<ContactEventLogic>> GetOneAsync(string id)
        {
            return await DoGetAsync<ContactEventLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/contactevent
        [HttpPost]
        [FwControllerMethod(Id:"MXJqxNoJC7nQ")]
        public async Task<ActionResult<ContactEventLogic>> PostAsync([FromBody]ContactEventLogic l)
        {
            return await DoPostAsync<ContactEventLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/contactevent/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"hCKaD0wqy4Pc")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync<ContactEventLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
