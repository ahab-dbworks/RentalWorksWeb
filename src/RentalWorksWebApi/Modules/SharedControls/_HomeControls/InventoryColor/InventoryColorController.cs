using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.HomeControls.InventoryColor
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"gJN4HKmkowSD")]
    public class InventoryColorController : AppDataController
    {
        public InventoryColorController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryColorLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorycolor/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"3XFlfB2PkBUo", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"wQUonE5X5yu7", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorycolor 
        [HttpGet]
        [FwControllerMethod(Id:"9badukFd84ZL", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<InventoryColorLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryColorLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorycolor/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"EhCDbbil9lbZ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<InventoryColorLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryColorLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorycolor 
        [HttpPost]
        [FwControllerMethod(Id:"Wrs0z5eykJ9z", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<InventoryColorLogic>> NewAsync([FromBody]InventoryColorLogic l)
        {
            return await DoNewAsync<InventoryColorLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/inventorycolor/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "CS63Fvc49l9GZ", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<InventoryColorLogic>> EditAsync([FromRoute] string id, [FromBody]InventoryColorLogic l)
        {
            return await DoEditAsync<InventoryColorLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorycolor/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"zJ9JHwKd9OZ3", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventoryColorLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
