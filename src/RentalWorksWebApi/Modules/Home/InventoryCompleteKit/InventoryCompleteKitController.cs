using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.InventoryCompleteKit
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class InventoryCompleteKitController : AppDataController
    {
        public InventoryCompleteKitController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryCompleteKitLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorycompletekit/browse 
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
        // GET api/v1/inventorycompletekit 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryCompleteKitLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryCompleteKitLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorycompletekit/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryCompleteKitLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryCompleteKitLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}