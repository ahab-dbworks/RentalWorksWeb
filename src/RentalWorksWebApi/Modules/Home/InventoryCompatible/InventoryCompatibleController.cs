using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;

namespace WebApi.Modules.Home.InventoryCompatible
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"mlAKf5gRPNNI")]
    public class InventoryCompatibleController : AppDataController
    {
        public InventoryCompatibleController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryCompatibleLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorycompatible/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"gGvMKoE3dBzL")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"uH1xBwPXgHZT")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorycompatible 
        [HttpGet]
        [FwControllerMethod(Id:"QlX874Ul7cup")]
        public async Task<ActionResult<IEnumerable<InventoryCompatibleLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryCompatibleLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorycompatible/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"cCixyLm2VaIa")]
        public async Task<ActionResult<InventoryCompatibleLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryCompatibleLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorycompatible 
        [HttpPost]
        [FwControllerMethod(Id:"LvhzZXhWbEBz")]
        public async Task<ActionResult<InventoryCompatibleLogic>> PostAsync([FromBody]InventoryCompatibleLogic l)
        {
            return await DoPostAsync<InventoryCompatibleLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorycompatible/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"Ks7UnEBkIU04")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventoryCompatibleLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}