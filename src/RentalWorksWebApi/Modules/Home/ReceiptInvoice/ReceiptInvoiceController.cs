using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;

namespace WebApi.Modules.Home.ReceiptInvoice
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class ReceiptInvoiceController : AppDataController
    {
        public ReceiptInvoiceController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ReceiptInvoiceLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/receiptinvoice/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "FQKNknibRlh")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/receiptinvoice/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "TxHBxvExNQV")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        //// GET api/v1/receiptinvoice 
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<ReceiptInvoiceLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        //{
        //    return await DoGetAsync<ReceiptInvoiceLogic>(pageno, pagesize, sort);
        //}
        ////------------------------------------------------------------------------------------ 
        //// GET api/v1/receiptinvoice/A0000001 
        //[HttpGet("{id}")]
        //public async Task<ActionResult<ReceiptInvoiceLogic>> GetOneAsync([FromRoute]string id)
        //{
        //    return await DoGetAsync<ReceiptInvoiceLogic>(id);
        //}
        ////------------------------------------------------------------------------------------ 
        //// POST api/v1/receiptinvoice 
        //[HttpPost]
        //public async Task<ActionResult<ReceiptInvoiceLogic>> PostAsync([FromBody]ReceiptInvoiceLogic l)
        //{
        //    return await DoPostAsync<ReceiptInvoiceLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/receiptinvoice/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ <ReceiptInvoiceLogic>
    }
}
