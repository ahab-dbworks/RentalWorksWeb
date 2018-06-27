using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.WallType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class WallTypeController : AppDataController
    {
        public WallTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WallTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/walltype/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(WallTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/walltype 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WallTypeLogic>(pageno, pagesize, sort, typeof(WallTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/walltype/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WallTypeLogic>(id, typeof(WallTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/walltype 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]WallTypeLogic l)
        {
            return await DoPostAsync<WallTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/walltype/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(WallTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}