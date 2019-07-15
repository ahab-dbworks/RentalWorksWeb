using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.PurchaseOrderReceiveBarCode
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"qH0cLrQVt9avI")]
    public class PurchaseOrderReceiveBarCodeController : AppDataController
    {
        public PurchaseOrderReceiveBarCodeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PurchaseOrderReceiveBarCodeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorderreceivebarcode/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"eC53vmVjaxes3")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorderreceivebarcode/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"PFTAyOtI28ow4")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/purchaseorderreceivebarcode 
        [HttpGet]
        [FwControllerMethod(Id:"QSIVtSyywADTB")]
        public async Task<ActionResult<IEnumerable<PurchaseOrderReceiveBarCodeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PurchaseOrderReceiveBarCodeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/purchaseorderreceivebarcode/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"C3KFH3rLySEPw")]
        public async Task<ActionResult<PurchaseOrderReceiveBarCodeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PurchaseOrderReceiveBarCodeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorderreceivebarcode 
        [HttpPost]
        [FwControllerMethod(Id:"x4mSJ73DiDmir")]
        public async Task<ActionResult<PurchaseOrderReceiveBarCodeLogic>> PostAsync([FromBody]PurchaseOrderReceiveBarCodeLogic l)
        {
            return await DoPostAsync<PurchaseOrderReceiveBarCodeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/purchaseorderreceivebarcode/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"oxf9uYnwgzCG0")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PurchaseOrderReceiveBarCodeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
