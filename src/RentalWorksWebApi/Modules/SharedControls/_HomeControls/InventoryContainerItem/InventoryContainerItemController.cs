using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.InventoryContainerItem
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
        [FwControllerMethod(Id:"qzPmKVg1fg5A")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"jo0z635RlaPJ")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorycontaineritem 
        [HttpGet]
        [FwControllerMethod(Id:"ZfkhqacIUiht")]
        public async Task<ActionResult<IEnumerable<InventoryContainerItemLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryContainerItemLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorycontaineritem/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"5chAqTavDuJW")]
        public async Task<ActionResult<InventoryContainerItemLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryContainerItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorycontaineritem 
        [HttpPost]
        [FwControllerMethod(Id:"7FjRslgrB0lt")]
        public async Task<ActionResult<InventoryContainerItemLogic>> PostAsync([FromBody]InventoryContainerItemLogic l)
        {
            return await DoPostAsync<InventoryContainerItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorycontaineritem/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"VKUlc3kysv6P")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventoryContainerItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
