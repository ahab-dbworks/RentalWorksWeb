using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.Building
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class BuildingController : AppDataController
    {
        public BuildingController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(BuildingLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/building/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(BuildingLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/building 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<BuildingLogic>(pageno, pagesize, sort, typeof(BuildingLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/building/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<BuildingLogic>(id, typeof(BuildingLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/building 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]BuildingLogic l)
        {
            return await DoPostAsync<BuildingLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/building/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(BuildingLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}