using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.MarketSegment
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class MarketSegmentController : AppDataController
    {
        public MarketSegmentController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(MarketSegmentLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/marketsegment/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(MarketSegmentLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/marketsegment 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<MarketSegmentLogic>(pageno, pagesize, sort, typeof(MarketSegmentLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/marketsegment/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<MarketSegmentLogic>(id, typeof(MarketSegmentLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/marketsegment 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]MarketSegmentLogic l)
        {
            return await DoPostAsync<MarketSegmentLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/marketsegment/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(MarketSegmentLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/marketsegment/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}
