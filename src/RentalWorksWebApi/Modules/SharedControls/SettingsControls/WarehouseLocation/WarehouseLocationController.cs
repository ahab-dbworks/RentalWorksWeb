using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;

namespace WebApi.Modules.Settings.WarehouseLocation
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"B1kMAlpwQNPLG")]
    public class WarehouseLocationController : AppDataController
    {
        public WarehouseLocationController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WarehouseLocationLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehouselocation/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"xSPKAIrjanNeQ", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"D1UIyYMSVtMM0", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehouselocation 
        [HttpGet]
        [FwControllerMethod(Id:"a435jjYplY6qa", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<WarehouseLocationLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WarehouseLocationLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehouselocation/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"TFN4jBbpzlf7Q", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<WarehouseLocationLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WarehouseLocationLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehouselocation 
        [HttpPost]
        [FwControllerMethod(Id:"r0xCAWQIwfFGO", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<WarehouseLocationLogic>> NewAsync([FromBody]WarehouseLocationLogic l)
        {
            return await DoNewAsync<WarehouseLocationLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/warehouselocation/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "qCDY6RU9WVBpp", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<WarehouseLocationLogic>> EditAsync([FromRoute] string id, [FromBody]WarehouseLocationLogic l)
        {
            return await DoEditAsync<WarehouseLocationLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/warehouselocation/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"7QR1igUbEX88B", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<WarehouseLocationLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehouselocation/validateofficelocation/browse
        [HttpPost("validateofficelocation/browse")]
        [FwControllerMethod(Id: "VvzsapysNsRn", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOfficeLocationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OfficeLocationLogic>(browseRequest);
        }
    }
}
