using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.WarehouseDepartment
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"BlB26FHHFsaQx")]
    public class WarehouseDepartmentController : AppDataController
    {
        public WarehouseDepartmentController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WarehouseDepartmentLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehousedepartment/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"fslJxg9QFGXO6", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"gfuZaQjMx5oEh", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehousedepartment 
        [HttpGet]
        [FwControllerMethod(Id:"LawhPUybU8uEb", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<WarehouseDepartmentLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WarehouseDepartmentLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehousedepartment/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"pAXJuO0TjPjla", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<WarehouseDepartmentLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WarehouseDepartmentLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehousedepartment 
        [HttpPost]
        [FwControllerMethod(Id:"FhCjLHW4NX0AL", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<WarehouseDepartmentLogic>> NewAsync([FromBody]WarehouseDepartmentLogic l)
        {
            return await DoNewAsync<WarehouseDepartmentLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/warehousedepartment/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "qA9nk0S5man4D", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<WarehouseDepartmentLogic>> EditAsync([FromRoute] string id, [FromBody]WarehouseDepartmentLogic l)
        {
            return await DoEditAsync<WarehouseDepartmentLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/warehousedepartment/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id:"B0ZF6lcjoR", ActionType: FwControllerActionTypes.Delete)]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await <WarehouseDepartmentLogic>DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
