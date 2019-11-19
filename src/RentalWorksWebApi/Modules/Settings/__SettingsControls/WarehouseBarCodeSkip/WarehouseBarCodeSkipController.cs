using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.WarehouseBarCodeSkip
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"LE9T9VOvHT73I")]
    public class WarehouseBarCodeSkipController : AppDataController
    {
        public WarehouseBarCodeSkipController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WarehouseBarCodeSkipLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehousebarcodeskip/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"qYitIFvZ6dOYy", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"3coBD9NkS0yeh", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehousebarcodeskip 
        [HttpGet]
        [FwControllerMethod(Id:"Jwr2X3DfTcE85", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<WarehouseBarCodeSkipLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WarehouseBarCodeSkipLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehousebarcodeskip/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"UIMbXMvHjeRhO", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<WarehouseBarCodeSkipLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WarehouseBarCodeSkipLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehousebarcodeskip 
        [HttpPost]
        [FwControllerMethod(Id:"GoBjMr4iIcTgi", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<WarehouseBarCodeSkipLogic>> NewAsync([FromBody]WarehouseBarCodeSkipLogic l)
        {
            return await DoNewAsync<WarehouseBarCodeSkipLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/warehousebarcodeskip/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "b8i9l0Z2XTI1Z", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<WarehouseBarCodeSkipLogic>> EditAsync([FromRoute] string id, [FromBody]WarehouseBarCodeSkipLogic l)
        {
            return await DoEditAsync<WarehouseBarCodeSkipLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/warehousebarcodeskip/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"OGUaAxnmf1Xv4", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<WarehouseBarCodeSkipLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
