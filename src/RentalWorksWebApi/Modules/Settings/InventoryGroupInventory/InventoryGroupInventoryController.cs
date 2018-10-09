using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.InventoryGroupInventory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class InventoryGroupInventoryController : AppDataController
    {
        public InventoryGroupInventoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryGroupInventoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorygroupinventory/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryGroupInventoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorygroupinventory 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryGroupInventoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryGroupInventoryLogic>(pageno, pagesize, sort, typeof(InventoryGroupInventoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorygroupinventory/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryGroupInventoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryGroupInventoryLogic>(id, typeof(InventoryGroupInventoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorygroupinventory 
        [HttpPost]
        public async Task<ActionResult<InventoryGroupInventoryLogic>> PostAsync([FromBody]InventoryGroupInventoryLogic l)
        {
            return await DoPostAsync<InventoryGroupInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorygroupinventory/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventoryGroupInventoryLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}