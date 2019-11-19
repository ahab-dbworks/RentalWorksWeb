using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.HomeControls.PurchaseOrderReceiveBarCode
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
        [FwControllerMethod(Id:"eC53vmVjaxes3", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorderreceivebarcode/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"PFTAyOtI28ow4", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/purchaseorderreceivebarcode 
        [HttpGet]
        [FwControllerMethod(Id:"QSIVtSyywADTB", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<PurchaseOrderReceiveBarCodeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PurchaseOrderReceiveBarCodeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/purchaseorderreceivebarcode/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"C3KFH3rLySEPw", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<PurchaseOrderReceiveBarCodeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PurchaseOrderReceiveBarCodeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorderreceivebarcode 
        [HttpPost]
        [FwControllerMethod(Id:"x4mSJ73DiDmir", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<PurchaseOrderReceiveBarCodeLogic>> NewAsync([FromBody]PurchaseOrderReceiveBarCodeLogic l)
        {
            return await DoNewAsync<PurchaseOrderReceiveBarCodeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/purchaseorderreceivebarcode/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "X3CGtX9VXwG4o", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<PurchaseOrderReceiveBarCodeLogic>> EditAsync([FromRoute] string id, [FromBody]PurchaseOrderReceiveBarCodeLogic l)
        {
            return await DoEditAsync<PurchaseOrderReceiveBarCodeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/purchaseorderreceivebarcode/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"oxf9uYnwgzCG0", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PurchaseOrderReceiveBarCodeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
