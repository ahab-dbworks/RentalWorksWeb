using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.HomeControls.InventoryPurchaseItem
{

    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "2Gl9Bispr91ox")]
    public class InventoryPurchaseItemController : AppDataController
    {
        public InventoryPurchaseItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryPurchaseItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorypurchaseitem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "rRBFYRQwIBr8b", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorypurchaseitem/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "4mBna1GKw1HBp", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorypurchaseitem 
        [HttpGet]
        [FwControllerMethod(Id: "1krbx7W2YgiG2", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<InventoryPurchaseItemLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryPurchaseItemLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorypurchaseitem/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "MZcWN6jTXnRR6", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<InventoryPurchaseItemLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryPurchaseItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/inventorypurchaseitem 
        //[HttpPost]
        //[FwControllerMethod(Id:"9NiRsOtV6ZyPm", ActionType: FwControllerActionTypes.New)]
        //public async Task<ActionResult<InventoryPurchaseItemLogic>> NewAsync([FromBody]InventoryPurchaseItemLogic l)
        //{
        //    return await DoNewAsync<InventoryPurchaseItemLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        // PUT api/v1/inventorypurchaseitem/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "LO4G6kMTosxwe", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<InventoryPurchaseItemLogic>> EditAsync([FromRoute] string id, [FromBody]InventoryPurchaseItemLogic l)
        {
            return await DoEditAsync<InventoryPurchaseItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorypurchaseitem/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"NHRl1EZnR1W9o", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventoryPurchaseItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
