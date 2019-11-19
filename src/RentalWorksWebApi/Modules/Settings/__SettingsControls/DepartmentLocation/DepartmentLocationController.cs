using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.DepartmentLocation
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"MFyU6dF7Lfsc")]
    public class DepartmentLocationController : AppDataController
    {
        public DepartmentLocationController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DepartmentLocationLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/departmentlocation/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"B9GO2IbH4tz5", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"wbdQhbgJ7EUh", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/departmentlocation 
        [HttpGet]
        [FwControllerMethod(Id:"NjFhDBZipjiM", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<DepartmentLocationLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DepartmentLocationLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/departmentlocation/A0000001~A0000002   (departmentid~locationid)
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"Tdmb4BtnZFk1", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DepartmentLocationLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DepartmentLocationLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/departmentlocation 
        [HttpPost]
        [FwControllerMethod(Id:"abMVWiv296OQ", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<DepartmentLocationLogic>> NewAsync([FromBody]DepartmentLocationLogic l)
        {
            return await DoNewAsync<DepartmentLocationLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/departmentlocation/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "fHheVnxqYpKWx", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<DepartmentLocationLogic>> EditAsync([FromRoute] string id, [FromBody]DepartmentLocationLogic l)
        {
            return await DoEditAsync<DepartmentLocationLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/departmentlocation/A0000001
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id:"rCC2SGeedrW5", ActionType: FwControllerActionTypes.Delete)]
        // public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        // {
        //     return await <DepartmentLocationLogic>DoDeleteAsync(id);
        // }
        // ------------------------------------------------------------------------------------ 
    }
}
