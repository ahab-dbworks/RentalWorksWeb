using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Home.InvoiceReceipt
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "cYUr48pou4fc")]
    public class InvoiceReceiptController : AppDataController
    {
        public InvoiceReceiptController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InvoiceReceiptLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoicereceipt/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "21qPBP87PwV8r")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoicereceipt/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "4AAOLrAlC8Io")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/invoicereceipt 
        [HttpGet]
        [FwControllerMethod(Id: "eGaIdr2iFeKnU")]
        public async Task<ActionResult<IEnumerable<InvoiceReceiptLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InvoiceReceiptLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/invoicereceipt/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "jWeE4W68kQcYJ")]
        public async Task<ActionResult<InvoiceReceiptLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InvoiceReceiptLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoicereceipt 
        [HttpPost]
        [FwControllerMethod(Id: "VKcdgeaWfLk7")]
        public async Task<ActionResult<InvoiceReceiptLogic>> PostAsync([FromBody]InvoiceReceiptLogic l)
        {
            return await DoPostAsync<InvoiceReceiptLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/invoicereceipt/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "8MoJ1YxBcA8p")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InvoiceReceiptLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
