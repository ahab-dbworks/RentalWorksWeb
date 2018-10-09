using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.InventoryColor
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class InventoryColorController : AppDataController
    {
        public InventoryColorController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryColorLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorycolor/browse 
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
        // GET api/v1/inventorycolor 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryColorLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryColorLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorycolor/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryColorLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryColorLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorycolor 
        [HttpPost]
        public async Task<ActionResult<InventoryColorLogic>> PostAsync([FromBody]InventoryColorLogic l)
        {
            return await DoPostAsync<InventoryColorLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorycolor/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}