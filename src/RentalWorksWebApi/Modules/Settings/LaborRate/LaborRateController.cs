using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.LaborRate
{
    [Route("api/v1/[controller]")]
    public class LaborRateController : AppDataController
    {
        public LaborRateController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(LaborRateLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/laborrate/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(LaborRateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/laborrate 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<LaborRateLogic>(pageno, pagesize, sort, typeof(LaborRateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/laborrate/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<LaborRateLogic>(id, typeof(LaborRateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/laborrate 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]LaborRateLogic l)
        {
            return await DoPostAsync<LaborRateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/laborrate/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(LaborRateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/laborrate/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
} 
