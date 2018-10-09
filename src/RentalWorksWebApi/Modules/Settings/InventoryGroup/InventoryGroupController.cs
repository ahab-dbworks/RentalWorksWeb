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
    public class InventoryGroupController : AppDataController
    {
        public InventoryGroupController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryGroupLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorygroup/browse 
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
        // GET api/v1/inventorygroup 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryGroupLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryGroupLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorygroup/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryGroupLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryGroupLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorygroup 
        [HttpPost]
        public async Task<ActionResult<InventoryGroupLogic>> PostAsync([FromBody]InventoryGroupLogic l)
        {
            return await DoPostAsync<InventoryGroupLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorygroup/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}