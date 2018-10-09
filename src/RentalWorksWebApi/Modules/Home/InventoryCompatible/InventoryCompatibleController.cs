using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;

namespace WebApi.Modules.Home.InventoryCompatible
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class InventoryCompatibleController : AppDataController
    {
        public InventoryCompatibleController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryCompatibleLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorycompatible/browse 
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
        // GET api/v1/inventorycompatible 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryCompatibleLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryCompatibleLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorycompatible/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryCompatibleLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryCompatibleLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorycompatible 
        [HttpPost]
        public async Task<ActionResult<InventoryCompatibleLogic>> PostAsync([FromBody]InventoryCompatibleLogic l)
        {
            return await DoPostAsync<InventoryCompatibleLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorycompatible/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}