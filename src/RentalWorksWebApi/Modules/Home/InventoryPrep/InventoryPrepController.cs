using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.InventoryPrep
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"CzNh6kOVsRO4")]
    public class InventoryPrepController : AppDataController
    {
        public InventoryPrepController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryPrepLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryprep/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"6wIOKW0e4xaG")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"r6RiGgk8NIRh")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryprep 
        [HttpGet]
        [FwControllerMethod(Id:"HLnogkW6DzPD")]
        public async Task<ActionResult<IEnumerable<InventoryPrepLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryPrepLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryprep/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"kBgv2sz7Bqo0")]
        public async Task<ActionResult<InventoryPrepLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryPrepLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryprep 
        [HttpPost]
        [FwControllerMethod(Id:"AtVqEe2TqEZX")]
        public async Task<ActionResult<InventoryPrepLogic>> PostAsync([FromBody]InventoryPrepLogic l)
        {
            return await DoPostAsync<InventoryPrepLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventoryprep/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"P9h0Q5Bv6cxx")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventoryPrepLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
