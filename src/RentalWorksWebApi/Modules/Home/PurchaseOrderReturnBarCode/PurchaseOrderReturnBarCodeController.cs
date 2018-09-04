using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.PurchaseOrderReturnBarCode
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class PurchaseOrderReturnBarCodeController : AppDataController
    {
        public PurchaseOrderReturnBarCodeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PurchaseOrderReturnBarCodeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorderreceivebarcode/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorderreceivebarcode/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/purchaseorderreceivebarcode 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PurchaseOrderReturnBarCodeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/purchaseorderreceivebarcode/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PurchaseOrderReturnBarCodeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/purchaseorderreceivebarcode 
        //[HttpPost]
        //public async Task<IActionResult> PostAsync([FromBody]PurchaseOrderReturnBarCodeLogic l)
        //{
        //    return await DoPostAsync<PurchaseOrderReturnBarCodeLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/purchaseorderreceivebarcode/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
