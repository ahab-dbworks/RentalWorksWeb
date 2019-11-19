using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.HomeControls.InventoryContainerItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"6ELSTtE6IqSb")]
    public class InventoryContainerItemController : AppDataController
    {
        public InventoryContainerItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryContainerItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorycontaineritem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"qzPmKVg1fg5A", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"jo0z635RlaPJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorycontaineritem 
        [HttpGet]
        [FwControllerMethod(Id:"ZfkhqacIUiht", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<InventoryContainerItemLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryContainerItemLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorycontaineritem/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"5chAqTavDuJW", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<InventoryContainerItemLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryContainerItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorycontaineritem 
        [HttpPost]
        [FwControllerMethod(Id:"7FjRslgrB0lt", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<InventoryContainerItemLogic>> NewAsync([FromBody]InventoryContainerItemLogic l)
        {
            return await DoNewAsync<InventoryContainerItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/inventorycontaineritem/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "RCVN1UcEgyESC", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<InventoryContainerItemLogic>> EditAsync([FromRoute] string id, [FromBody]InventoryContainerItemLogic l)
        {
            return await DoEditAsync<InventoryContainerItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorycontaineritem/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"VKUlc3kysv6P", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventoryContainerItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
