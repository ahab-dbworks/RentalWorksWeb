using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.HomeControls.InventoryPrep
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"CzNh6kOVsRO4")]
    public class InventoryPrepController : AppDataController
    {
        public InventoryPrepController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryPrepLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryprep/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"6wIOKW0e4xaG", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"r6RiGgk8NIRh", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryprep 
        [HttpGet]
        [FwControllerMethod(Id:"HLnogkW6DzPD", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<InventoryPrepLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryPrepLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryprep/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"kBgv2sz7Bqo0", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<InventoryPrepLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryPrepLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryprep 
        [HttpPost]
        [FwControllerMethod(Id:"AtVqEe2TqEZX", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<InventoryPrepLogic>> NewAsync([FromBody]InventoryPrepLogic l)
        {
            return await DoNewAsync<InventoryPrepLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/inventoryprep/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "hXWaIXl5oJhT2", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<InventoryPrepLogic>> EditAsync([FromRoute] string id, [FromBody]InventoryPrepLogic l)
        {
            return await DoEditAsync<InventoryPrepLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventoryprep/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"P9h0Q5Bv6cxx", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventoryPrepLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
