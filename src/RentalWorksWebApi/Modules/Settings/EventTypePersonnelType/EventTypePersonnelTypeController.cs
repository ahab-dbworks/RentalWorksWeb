using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.EventTypePersonnelType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class EventTypePersonnelTypeController : AppDataController
    {
        public EventTypePersonnelTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(EventTypePersonnelTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/eventtypepersonneltype/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(EventTypePersonnelTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/eventtypepersonneltype 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<EventTypePersonnelTypeLogic>(pageno, pagesize, sort, typeof(EventTypePersonnelTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/eventtypepersonneltype/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<EventTypePersonnelTypeLogic>(id, typeof(EventTypePersonnelTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/eventtypepersonneltype 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]EventTypePersonnelTypeLogic l)
        {
            return await DoPostAsync<EventTypePersonnelTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/eventtypepersonneltype/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(EventTypePersonnelTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}