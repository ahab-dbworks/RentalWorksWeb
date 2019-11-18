using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.InventoryGroup
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
        [FwControllerMethod(Id:"0UvSvmimIhY0")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"n6L8MVm5nfxZ")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorygroup 
        [HttpGet]
        [FwControllerMethod(Id:"Djczsc2xjtot")]
        public async Task<ActionResult<IEnumerable<InventoryGroupLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryGroupLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorygroup/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"zmGwfSUubm2a")]
        public async Task<ActionResult<InventoryGroupLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryGroupLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorygroup 
        [HttpPost]
        [FwControllerMethod(Id:"qnCrIuRVrJYZ")]
        public async Task<ActionResult<InventoryGroupLogic>> PostAsync([FromBody]InventoryGroupLogic l)
        {
            return await DoPostAsync<InventoryGroupLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorygroup/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"aQ4umKCed68D")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventoryGroupLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
