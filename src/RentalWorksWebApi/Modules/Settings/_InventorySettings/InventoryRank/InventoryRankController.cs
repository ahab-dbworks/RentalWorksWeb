using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.InventorySettings.InventoryRank
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"3YXhU6x3GseH")]
    public class InventoryRankController : AppDataController
    {
        public InventoryRankController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryRankLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryrank/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"lKvnchXC8OUn")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"FAwONeudHbyN")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryrank 
        [HttpGet]
        [FwControllerMethod(Id:"FHs5gPJJ1v6H")]
        public async Task<ActionResult<IEnumerable<InventoryRankLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryRankLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryrank/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"dg11oj8gp9Xh")]
        public async Task<ActionResult<InventoryRankLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryRankLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryrank 
        [HttpPost]
        [FwControllerMethod(Id:"mNm8c55bonsm")]
        public async Task<ActionResult<InventoryRankLogic>> PostAsync([FromBody]InventoryRankLogic l)
        {
            return await DoPostAsync<InventoryRankLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventoryrank/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"I0eetYvV8SZl")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventoryRankLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
