using FwStandard.AppManager;
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
    [FwController(Id:"ABL0XJQpsQQo")]
    public class InventoryPackageInventoryController : AppDataController
    {
        public InventoryPackageInventoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryPackageInventoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorypackageinventory/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"hjMyubido3ma")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"rz1vur2Bi7XN")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorypackageinventory 
        [HttpGet]
        [FwControllerMethod(Id:"TlB11czcREeK")]
        public async Task<ActionResult<IEnumerable<InventoryPackageInventoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryPackageInventoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorypackageinventory/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"ApDS0FJu8D48")]
        public async Task<ActionResult<InventoryPackageInventoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryPackageInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorypackageinventory 
        [HttpPost]
        [FwControllerMethod(Id:"EKGGGkcY7321")]
        public async Task<ActionResult<InventoryPackageInventoryLogic>> PostAsync([FromBody]InventoryPackageInventoryLogic l)
        {
            return await DoPostAsync<InventoryPackageInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorypackageinventory/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"wABYpEfBSzxd")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventoryPackageInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
