using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.HomeControls.InventorySubstitute
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"5sN9zKtGzNTq")]
    public class InventorySubstituteController : AppDataController
    {
        public InventorySubstituteController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventorySubstituteLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysubstitute/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"LfWL5rzpIXJa", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"ftnzkHO7jdyZ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorysubstitute 
        [HttpGet]
        [FwControllerMethod(Id:"tqWpJOEctnrx", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<InventorySubstituteLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventorySubstituteLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorysubstitute/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"1f1oEInbTnfH", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<InventorySubstituteLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventorySubstituteLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysubstitute 
        [HttpPost]
        [FwControllerMethod(Id:"5rr0TsYaRhhc", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<InventorySubstituteLogic>> NewAsync([FromBody]InventorySubstituteLogic l)
        {
            return await DoNewAsync<InventorySubstituteLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/inventorysubstitute/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "iV02Ku0n4BVKl", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<InventorySubstituteLogic>> EditAsync([FromRoute] string id, [FromBody]InventorySubstituteLogic l)
        {
            return await DoEditAsync<InventorySubstituteLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorysubstitute/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"eaG1OvC6g3xd", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventorySubstituteLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
