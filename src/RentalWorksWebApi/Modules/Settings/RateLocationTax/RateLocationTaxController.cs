using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.RateLocationTax
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class RateLocationTaxController : AppDataController
    {
        public RateLocationTaxController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RateLocationTaxLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ratelocationtax/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(RateLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ratelocationtax 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RateLocationTaxLogic>(pageno, pagesize, sort, typeof(RateLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ratelocationtax/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<RateLocationTaxLogic>(id, typeof(RateLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ratelocationtax 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]RateLocationTaxLogic l)
        {
            return await DoPostAsync<RateLocationTaxLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ratelocationtax/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(RateLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}