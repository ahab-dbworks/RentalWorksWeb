using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Inventory.Purchase
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "inventory-v1")]
    [FwController(Id: "8XKYiQYXj9BKK")]
    public class PurchaseController : AppDataController
    {
        public PurchaseController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PurchaseLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchase/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "8xRNUpaPrGMBP", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchase/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "8XWvTqvya5V74", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/purchase 
        [HttpGet]
        [FwControllerMethod(Id: "8Yg63JN5zO1pb", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<PurchaseLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PurchaseLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/purchase/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "8YmoSbjvy0x8n", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<PurchaseLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PurchaseLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        //No support for NEW.  Use the Inventory Purchase Utility instead
        //// POST api/v1/purchase 
        //[HttpPost]
        //[FwControllerMethod(Id: "8zcNl2OnF1EM0", ActionType: FwControllerActionTypes.New)]
        //public async Task<ActionResult<PurchaseLogic>> NewAsync([FromBody]PurchaseLogic l)
        //{
        //    return await DoNewAsync<PurchaseLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //Limited support for Edits. (ie. modifying notes, etc)
        // PUT api/v1/purchase/A0000001 
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "90aXu2WrzPLXg", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<PurchaseLogic>> EditAsync([FromRoute] string id, [FromBody]PurchaseLogic l)
        {
            return await DoEditAsync<PurchaseLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //No support for DELETE.  Use Retire instead
        //// DELETE api/v1/purchase/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id: "90mBHLbkVNF5b", ActionType: FwControllerActionTypes.Delete)]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync<PurchaseLogic>(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
