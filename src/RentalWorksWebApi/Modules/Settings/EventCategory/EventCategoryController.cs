using FwStandard.SqlServer;
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
    public class EventCategoryController : AppDataController
    {
        public EventCategoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(EventCategoryLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/eventcategory/browse
        [HttpPost("browse")]
        [Authorize(Policy = "{C9D7D24A-2CD3-4979-B6BC-B9DDF4070AAF}")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(EventCategoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/eventcategory
        [HttpGet]
        [Authorize(Policy = "{08DE89EE-4C55-4170-B1D8-33EC04F2745C}")]
        public async Task<ActionResult<IEnumerable<EventCategoryLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<EventCategoryLogic>(pageno, pagesize, sort, typeof(EventCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/eventcategory/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "{ED9F7706-C1C0-41B7-8A05-BD2BCB224DF4}")]
        public async Task<ActionResult<EventCategoryLogic>> GetOneAsync(string id)
        {
            return await DoGetAsync<EventCategoryLogic>(id, typeof(EventCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/eventcategory
        [HttpPost]
        [Authorize(Policy = "{FADA1CF8-C517-4A23-966B-97E7A784B16B}")]
        public async Task<ActionResult<EventCategoryLogic>> PostAsync([FromBody]EventCategoryLogic l)
        {
            return await DoPostAsync<EventCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/eventcategory/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{7EFD1410-B290-4C6F-8AF4-61D42667E7D7}")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(EventCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}