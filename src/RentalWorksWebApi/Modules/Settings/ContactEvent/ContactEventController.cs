using FwStandard.SqlServer;
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
    public class ContactEventController : AppDataController
    {
        public ContactEventController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ContactEventLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/contactevent/browse
        [HttpPost("browse")]
        [Authorize(Policy = "{5973FA5B-5519-45DC-9ABF-EF6AF65471C1}")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ContactEventLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/contactevent
        [HttpGet]
        [Authorize(Policy = "{1415227E-1A40-4492-B519-462EC788CDE1}")]
        public async Task<ActionResult<IEnumerable<ContactEventLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<ContactEventLogic>(pageno, pagesize, sort, typeof(ContactEventLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/contactevent/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "{15A4DD14-CE3C-454E-B475-62B6BE30081F}")]
        public async Task<ActionResult<ContactEventLogic>> GetOneAsync(string id)
        {
            return await DoGetAsync<ContactEventLogic>(id, typeof(ContactEventLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/contactevent
        [HttpPost]
        [Authorize(Policy = "{35C46276-BBB7-43ED-BBE6-98FFB12655DC}")]
        public async Task<ActionResult<ContactEventLogic>> PostAsync([FromBody]ContactEventLogic l)
        {
            return await DoPostAsync<ContactEventLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/contactevent/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{A2EC754D-B1AB-4355-8803-30DF1D42B49D}")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(ContactEventLogic));
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}