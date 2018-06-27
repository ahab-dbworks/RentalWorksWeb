using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.Floor
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class FloorController : AppDataController
    {
        public FloorController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(FloorLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/floor/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(FloorLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/floor 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FloorLogic>(pageno, pagesize, sort, typeof(FloorLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/floor/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<FloorLogic>(id, typeof(FloorLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/floor 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]FloorLogic l)
        {
            return await DoPostAsync<FloorLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/floor/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(FloorLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}