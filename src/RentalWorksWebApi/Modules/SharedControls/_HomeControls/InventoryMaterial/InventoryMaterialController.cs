using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.InventoryMaterial
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"l35woZUn3E5M")]
    public class InventoryMaterialController : AppDataController
    {
        public InventoryMaterialController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryMaterialLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorymaterial/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"CAN8uJMwAECp")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"VFqmItcNEiCA")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorymaterial 
        [HttpGet]
        [FwControllerMethod(Id:"Pbb5zxRL0lSN")]
        public async Task<ActionResult<IEnumerable<InventoryMaterialLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryMaterialLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorymaterial/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"qTv74yhhfk1c")]
        public async Task<ActionResult<InventoryMaterialLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryMaterialLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorymaterial 
        [HttpPost]
        [FwControllerMethod(Id:"YozvCGCu2Gxr")]
        public async Task<ActionResult<InventoryMaterialLogic>> PostAsync([FromBody]InventoryMaterialLogic l)
        {
            return await DoPostAsync<InventoryMaterialLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorymaterial/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"8F89eZz2E2M0")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventoryMaterialLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
