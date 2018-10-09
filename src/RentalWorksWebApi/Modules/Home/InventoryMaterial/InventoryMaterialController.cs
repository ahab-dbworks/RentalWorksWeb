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
    public class InventoryMaterialController : AppDataController
    {
        public InventoryMaterialController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryMaterialLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorymaterial/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryMaterialLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorymaterial 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryMaterialLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryMaterialLogic>(pageno, pagesize, sort, typeof(InventoryMaterialLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorymaterial/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryMaterialLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryMaterialLogic>(id, typeof(InventoryMaterialLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorymaterial 
        [HttpPost]
        public async Task<ActionResult<InventoryMaterialLogic>> PostAsync([FromBody]InventoryMaterialLogic l)
        {
            return await DoPostAsync<InventoryMaterialLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorymaterial/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventoryMaterialLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}