using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.SpaceType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class SpaceTypeController : AppDataController
    {
        public SpaceTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SpaceTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/spacetype/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(SpaceTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/spacetype 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SpaceTypeLogic>(pageno, pagesize, sort, typeof(SpaceTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/spacetype/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SpaceTypeLogic>(id, typeof(SpaceTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/spacetype 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]SpaceTypeLogic l)
        {
            return await DoPostAsync<SpaceTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/spacetype/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(SpaceTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}