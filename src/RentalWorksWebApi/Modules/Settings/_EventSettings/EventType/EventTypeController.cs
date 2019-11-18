using FwStandard.AppManager;
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
    [FwController(Id:"HXotrQfoaQCq")]
    public class EventTypeController : AppDataController
    {
        public EventTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(EventTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/eventtype/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"VhQTPcr2nIPx")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"3388KPzfFwT3")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/eventtype 
        [HttpGet]
        [FwControllerMethod(Id:"NU11TCmD5qpn")]
        public async Task<ActionResult<IEnumerable<EventTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<EventTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/eventtype/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"xczlCq8V19z4")]
        public async Task<ActionResult<EventTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<EventTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/eventtype 
        [HttpPost]
        [FwControllerMethod(Id:"kqLxq9jdYnEC")]
        public async Task<ActionResult<EventTypeLogic>> PostAsync([FromBody]EventTypeLogic l)
        {
            return await DoPostAsync<EventTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/eventtype/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"qW1IDgSIYrjp")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<EventTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
