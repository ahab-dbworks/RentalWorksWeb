using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
namespace WebApi.Modules.HomeControls.PurchaseVendorInvoiceItem
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
        [FwControllerMethod(Id: "nr00NURO5iIY", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchasevendorinvoiceitem/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "wkjXX3bTxP44", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/purchasevendorinvoiceitem 
        [HttpGet]
        [FwControllerMethod(Id: "XE66f1skGqzso", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<PurchaseVendorInvoiceItemLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PurchaseVendorInvoiceItemLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/purchasevendorinvoiceitem/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "bpK9l8JcaybLW", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<PurchaseVendorInvoiceItemLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PurchaseVendorInvoiceItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/purchasevendorinvoiceitem 
        //[HttpPost]
        //[FwControllerMethod(Id: "fLRniDeeCQzz", ActionType: FwControllerActionTypes.Edit)]
        //public async Task<ActionResult<PurchaseVendorInvoiceItemLogic>> PostAsync([FromBody]PurchaseVendorInvoiceItemLogic l)
        //{
        //    return await DoPostAsync<PurchaseVendorInvoiceItemLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/purchasevendorinvoiceitem/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id: "eJAHVCH4hZoU", ActionType: FwControllerActionTypes.Delete)]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await <PurchaseVendorInvoiceItemLogic>DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
