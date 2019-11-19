using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.InventorySettings.WarehouseCatalog
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"wMXhVrm9w33xO")]
    public class WarehouseCatalogController : AppDataController
    {
        public WarehouseCatalogController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WarehouseCatalogLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehousecatalog/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"bGXrJikz6Wy25", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"9n0Sl4PrlWyW6", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehousecatalog 
        [HttpGet]
        [FwControllerMethod(Id:"ne3Ltlc3d5Eeg", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<WarehouseCatalogLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WarehouseCatalogLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehousecatalog/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"rDzjmIL6Hr9Z8", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<WarehouseCatalogLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WarehouseCatalogLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehousecatalog 
        [HttpPost]
        [FwControllerMethod(Id:"gwU2zafipLMpN", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<WarehouseCatalogLogic>> NewAsync([FromBody]WarehouseCatalogLogic l)
        {
            return await DoNewAsync<WarehouseCatalogLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/warehousecatalog/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "IoEL86Q1ftQMU", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<WarehouseCatalogLogic>> EditAsync([FromRoute] string id, [FromBody]WarehouseCatalogLogic l)
        {
            return await DoEditAsync<WarehouseCatalogLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/warehousecatalog/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"RVaiotb0qL8O3", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<WarehouseCatalogLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
