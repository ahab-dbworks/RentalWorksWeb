using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.SpaceRate
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class SpaceRateController : AppDataController
    {
        public SpaceRateController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SpaceRateLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/spacerate/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(SpaceRateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/spacerate 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SpaceRateLogic>(pageno, pagesize, sort, typeof(SpaceRateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/spacerate/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<SpaceRateLogic>(id, typeof(SpaceRateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/spacerate 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]SpaceRateLogic l)
        {
            return await DoPostAsync<SpaceRateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/spacerate/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(SpaceRateLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}