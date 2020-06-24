using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Inventory.InventoryAdjustment
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "inventory-v1")]
    [FwController(Id: "s1R2anReJAUoU")]
    public class InventoryAdjustmentController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public InventoryAdjustmentController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryAdjustmentLogic); }
        //------------------------------------------------------------------------------------ 
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryadjustment/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "S284uxgj4ttWb", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryadjustment/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "s2CmFeImKfUH0", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryadjustment 
        [HttpGet]
        [FwControllerMethod(Id: "s2eSUPAInVJsX", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<InventoryAdjustmentLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryAdjustmentLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryadjustment/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "s4ObwRmlxIaJa", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<InventoryAdjustmentLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryAdjustmentLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryadjustment 
        [HttpPost]
        [FwControllerMethod(Id: "S4qwMZgEuGBmR", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<InventoryAdjustmentLogic>> NewAsync([FromBody]InventoryAdjustmentLogic l)
        {
            return await DoNewAsync<InventoryAdjustmentLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// PUT api/v1/inventoryadjustment/A0000001 
        //
        // editing is prohibited
        //
        //[HttpPut("{id}")]
        //[FwControllerMethod(Id: "S5KyNp2DGXLnE", ActionType: FwControllerActionTypes.Edit)]
        //public async Task<ActionResult<InventoryAdjustmentLogic>> EditAsync([FromRoute] string id, [FromBody]InventoryAdjustmentLogic l)
        //{
        //    return await DoEditAsync<InventoryAdjustmentLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/inventoryadjustment/A0000001 
        //
        // deleting is prohibited
        //
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id: "S63wfHZRyIV6C", ActionType: FwControllerActionTypes.Delete)]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync<InventoryAdjustmentLogic>(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
