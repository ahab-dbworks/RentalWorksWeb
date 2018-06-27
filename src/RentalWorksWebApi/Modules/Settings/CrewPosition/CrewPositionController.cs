using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.CrewPosition
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class CrewPositionController : AppDataController
    {
        public CrewPositionController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CrewPositionLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/crewposition/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(CrewPositionLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/crewposition 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CrewPositionLogic>(pageno, pagesize, sort, typeof(CrewPositionLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/crewposition/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CrewPositionLogic>(id, typeof(CrewPositionLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/crewposition 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]CrewPositionLogic l)
        {
            return await DoPostAsync<CrewPositionLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/crewposition/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(CrewPositionLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}