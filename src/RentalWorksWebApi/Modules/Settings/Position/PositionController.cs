using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.Position
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class PositionController : AppDataController
    {
        public PositionController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PositionLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/position/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PositionLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/position 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PositionLogic>(pageno, pagesize, sort, typeof(PositionLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/position/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<PositionLogic>(id, typeof(PositionLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/position 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]PositionLogic l)
        {
            return await DoPostAsync<PositionLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/position/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(PositionLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
} 
