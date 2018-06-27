using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.CrewLocation
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class CrewLocationController : AppDataController
    {
        public CrewLocationController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CrewLocationLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/crewlocation/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(CrewLocationLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/crewlocation 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CrewLocationLogic>(pageno, pagesize, sort, typeof(CrewLocationLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/crewlocation/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CrewLocationLogic>(id, typeof(CrewLocationLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/crewlocation 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]CrewLocationLogic l)
        {
            return await DoPostAsync<CrewLocationLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/crewlocation/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(CrewLocationLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}