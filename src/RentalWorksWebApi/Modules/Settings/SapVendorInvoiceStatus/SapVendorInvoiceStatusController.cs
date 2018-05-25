using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.SapVendorInvoiceStatus
{
    [Route("api/v1/[controller]")]
    public class SapVendorInvoiceStatusController : AppDataController
    {
        public SapVendorInvoiceStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SapVendorInvoiceStatusLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/sapvendorinvoicestatus/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(SapVendorInvoiceStatusLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/sapvendorinvoicestatus 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SapVendorInvoiceStatusLogic>(pageno, pagesize, sort, typeof(SapVendorInvoiceStatusLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/sapvendorinvoicestatus/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<SapVendorInvoiceStatusLogic>(id, typeof(SapVendorInvoiceStatusLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/sapvendorinvoicestatus 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]SapVendorInvoiceStatusLogic l)
        {
            return await DoPostAsync<SapVendorInvoiceStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/sapvendorinvoicestatus/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(SapVendorInvoiceStatusLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/sapvendorinvoicestatus/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}