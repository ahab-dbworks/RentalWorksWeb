using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.MarketType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class MarketTypeController : AppDataController
    {
        public MarketTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(MarketTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/markettype/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(MarketTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/markettype 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<MarketTypeLogic>(pageno, pagesize, sort, typeof(MarketTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/markettype/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<MarketTypeLogic>(id, typeof(MarketTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/markettype 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]MarketTypeLogic l)
        {
            return await DoPostAsync<MarketTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/markettype/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(MarketTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/markettype/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}
