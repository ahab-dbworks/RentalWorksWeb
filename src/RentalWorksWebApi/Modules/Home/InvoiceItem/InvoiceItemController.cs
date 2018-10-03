using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.InvoiceItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class InvoiceItemController : AppDataController
    {
        public InvoiceItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InvoiceItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoiceitem/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoiceitem/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/invoiceitem 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InvoiceItemLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/invoiceitem/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InvoiceItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoiceitem 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]InvoiceItemLogic l)
        {
            return await DoPostAsync<InvoiceItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/invoiceitem/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
