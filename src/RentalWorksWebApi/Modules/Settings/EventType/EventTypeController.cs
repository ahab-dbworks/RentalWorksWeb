using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.EventType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class EventTypeController : AppDataController
    {
        public EventTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(EventTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/eventtype/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(EventTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/eventtype 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<EventTypeLogic>(pageno, pagesize, sort, typeof(EventTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/eventtype/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<EventTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<EventTypeLogic>(id, typeof(EventTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/eventtype 
        [HttpPost]
        public async Task<ActionResult<EventTypeLogic>> PostAsync([FromBody]EventTypeLogic l)
        {
            return await DoPostAsync<EventTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/eventtype/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(EventTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}