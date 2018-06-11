using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.VendorInvoiceApprover
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class VendorInvoiceApproverController : AppDataController
    {
        public VendorInvoiceApproverController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VendorInvoiceApproverLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoiceapprover/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(VendorInvoiceApproverLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/vendorinvoiceapprover 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VendorInvoiceApproverLogic>(pageno, pagesize, sort, typeof(VendorInvoiceApproverLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/vendorinvoiceapprover/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<VendorInvoiceApproverLogic>(id, typeof(VendorInvoiceApproverLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoiceapprover 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]VendorInvoiceApproverLogic l)
        {
            return await DoPostAsync<VendorInvoiceApproverLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/vendorinvoiceapprover/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(VendorInvoiceApproverLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}