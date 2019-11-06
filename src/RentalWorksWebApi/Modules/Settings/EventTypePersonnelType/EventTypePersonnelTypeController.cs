using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.EventTypePersonnelType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"CbjLxfIjRyg")]
    public class EventTypePersonnelTypeController : AppDataController
    {
        public EventTypePersonnelTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(EventTypePersonnelTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/eventtypepersonneltype/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"T3eH8oLs4Is")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"zyDFqmAvuBF")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/eventtypepersonneltype 
        [HttpGet]
        [FwControllerMethod(Id:"HCDvZsi8mjF")]
        public async Task<ActionResult<IEnumerable<EventTypePersonnelTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<EventTypePersonnelTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/eventtypepersonneltype/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"wI12JljuTXB")]
        public async Task<ActionResult<EventTypePersonnelTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<EventTypePersonnelTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/eventtypepersonneltype 
        [HttpPost]
        [FwControllerMethod(Id:"JysKGfAOWwS")]
        public async Task<ActionResult<EventTypePersonnelTypeLogic>> PostAsync([FromBody]EventTypePersonnelTypeLogic l)
        {
            return await DoPostAsync<EventTypePersonnelTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/eventtypepersonneltype/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"1vNsKRaLbw8")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<EventTypePersonnelTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
