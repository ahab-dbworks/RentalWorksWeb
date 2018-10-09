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
    public class InventoryContainerItemController : AppDataController
    {
        public InventoryContainerItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryContainerItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorycontaineritem/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorycontaineritem 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryContainerItemLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryContainerItemLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorycontaineritem/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryContainerItemLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryContainerItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorycontaineritem 
        [HttpPost]
        public async Task<ActionResult<InventoryContainerItemLogic>> PostAsync([FromBody]InventoryContainerItemLogic l)
        {
            return await DoPostAsync<InventoryContainerItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorycontaineritem/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}