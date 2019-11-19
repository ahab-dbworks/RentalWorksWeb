using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.HomeControls.InventoryConsignor
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"JKfdyoLXFqu3")]
    public class InventoryConsignorController : AppDataController
    {
        public InventoryConsignorController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryConsignorLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryconsignor/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"2UI7L4HnbhdQ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"X8NkHBkNXdck", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryconsignor 
        [HttpGet]
        [FwControllerMethod(Id:"7cJyGnOo1DUf", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<InventoryConsignorLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryConsignorLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryconsignor/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"jea9oq6UO6Bv", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<InventoryConsignorLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryConsignorLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
