using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.InventoryPackageInventory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class InventoryPackageInventoryController : AppDataController
    {
        public InventoryPackageInventoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryPackageInventoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorypackageinventory/browse 
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
        // GET api/v1/inventorypackageinventory 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryPackageInventoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryPackageInventoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorypackageinventory/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryPackageInventoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryPackageInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorypackageinventory 
        [HttpPost]
        public async Task<ActionResult<InventoryPackageInventoryLogic>> PostAsync([FromBody]InventoryPackageInventoryLogic l)
        {
            return await DoPostAsync<InventoryPackageInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorypackageinventory/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}