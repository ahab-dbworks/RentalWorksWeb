using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.InventoryRank
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class InventoryRankController : AppDataController
    {
        public InventoryRankController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryRankLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryrank/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryRankLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryrank 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryRankLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryRankLogic>(pageno, pagesize, sort, typeof(InventoryRankLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryrank/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryRankLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryRankLogic>(id, typeof(InventoryRankLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryrank 
        [HttpPost]
        public async Task<ActionResult<InventoryRankLogic>> PostAsync([FromBody]InventoryRankLogic l)
        {
            return await DoPostAsync<InventoryRankLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventoryrank/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventoryRankLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}