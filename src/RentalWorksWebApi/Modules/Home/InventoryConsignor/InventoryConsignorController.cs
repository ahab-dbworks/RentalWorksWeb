using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.InventoryConsignor
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class InventoryConsignorController : AppDataController
    {
        public InventoryConsignorController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryConsignorLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryconsignor/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryConsignorLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryconsignor 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryConsignorLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryConsignorLogic>(pageno, pagesize, sort, typeof(InventoryConsignorLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryconsignor/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryConsignorLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryConsignorLogic>(id, typeof(InventoryConsignorLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}