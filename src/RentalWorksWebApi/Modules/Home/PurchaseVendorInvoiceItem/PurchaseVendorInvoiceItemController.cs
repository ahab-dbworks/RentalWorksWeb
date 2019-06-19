using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
namespace WebApi.Modules.Home.PurchaseVendorInvoiceItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "NlKSJj2fN0ly")]
    public class PurchaseVendorInvoiceItemController : AppDataController
    {
        public PurchaseVendorInvoiceItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PurchaseVendorInvoiceItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchasevendorinvoiceitem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "nr00NURO5iIY")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchasevendorinvoiceitem/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "wkjXX3bTxP44")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/purchasevendorinvoiceitem 
        [HttpGet]
        [FwControllerMethod(Id: "XE66f1skGqzso")]
        public async Task<ActionResult<IEnumerable<PurchaseVendorInvoiceItemLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PurchaseVendorInvoiceItemLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/purchasevendorinvoiceitem/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "bpK9l8JcaybLW")]
        public async Task<ActionResult<PurchaseVendorInvoiceItemLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PurchaseVendorInvoiceItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchasevendorinvoiceitem 
        [HttpPost]
        [FwControllerMethod(Id: "fLRniDeeCQzz")]
        public async Task<ActionResult<PurchaseVendorInvoiceItemLogic>> PostAsync([FromBody]PurchaseVendorInvoiceItemLogic l)
        {
            return await DoPostAsync<PurchaseVendorInvoiceItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/purchasevendorinvoiceitem/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "eJAHVCH4hZoU")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
