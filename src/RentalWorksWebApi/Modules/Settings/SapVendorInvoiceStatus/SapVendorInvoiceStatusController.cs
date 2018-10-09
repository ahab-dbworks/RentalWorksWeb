using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.SapVendorInvoiceStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class SapVendorInvoiceStatusController : AppDataController
    {
        public SapVendorInvoiceStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SapVendorInvoiceStatusLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/sapvendorinvoicestatus/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/sapvendorinvoicestatus 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SapVendorInvoiceStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SapVendorInvoiceStatusLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/sapvendorinvoicestatus/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<SapVendorInvoiceStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SapVendorInvoiceStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/sapvendorinvoicestatus 
        [HttpPost]
        public async Task<ActionResult<SapVendorInvoiceStatusLogic>> PostAsync([FromBody]SapVendorInvoiceStatusLogic l)
        {
            return await DoPostAsync<SapVendorInvoiceStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/sapvendorinvoicestatus/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}