using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.InventorySettings.InventoryGroup
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"XgfGFSbbiiHy")]
    public class InventoryGroupController : AppDataController
    {
        public InventoryGroupController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryGroupLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorygroup/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"0UvSvmimIhY0", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"n6L8MVm5nfxZ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorygroup 
        [HttpGet]
        [FwControllerMethod(Id:"Djczsc2xjtot", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<InventoryGroupLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryGroupLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorygroup/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"zmGwfSUubm2a", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<InventoryGroupLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryGroupLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorygroup 
        [HttpPost]
        [FwControllerMethod(Id:"qnCrIuRVrJYZ", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<InventoryGroupLogic>> NewAsync([FromBody]InventoryGroupLogic l)
        {
            return await DoNewAsync<InventoryGroupLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/inventorygroup/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "cfujzk9DPS7BH", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<InventoryGroupLogic>> EditAsync([FromRoute] string id, [FromBody]InventoryGroupLogic l)
        {
            return await DoEditAsync<InventoryGroupLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorygroup/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"aQ4umKCed68D", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventoryGroupLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
