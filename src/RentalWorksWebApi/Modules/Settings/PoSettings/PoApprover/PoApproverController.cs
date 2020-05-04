using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using WebApi.Modules.Administrator.User;
using WebApi.Modules.Settings.PoSettings.PoApproverRole;
using WebApi.Modules.Settings.DepartmentSettings.Department;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;

namespace WebApi.Modules.Settings.PoSettings.PoApprover
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"kaGlUrLG9GjN")]
    public class PoApproverController : AppDataController
    {
        public PoApproverController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PoApproverLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/poapprover/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"QhSyIviB2Xmn", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"5PV59EQmvAf0", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/poapprover 
        [HttpGet]
        [FwControllerMethod(Id:"1K60SzR2NbEx", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<PoApproverLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PoApproverLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/poapprover/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"F24bmvivCYf4", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<PoApproverLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PoApproverLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/poapprover 
        [HttpPost]
        [FwControllerMethod(Id:"NQQ7hDodlcRn", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<PoApproverLogic>> NewAsync([FromBody]PoApproverLogic l)
        {
            return await DoNewAsync<PoApproverLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/poapprover/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "72mM4GqknwNXq", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<PoApproverLogic>> EditAsync([FromRoute] string id, [FromBody]PoApproverLogic l)
        {
            return await DoEditAsync<PoApproverLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/poapprover/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"AoMJdHYdcTVw", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PoApproverLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/poapprover/validateuser/browse
        [HttpPost("validateuser/browse")]
        [FwControllerMethod(Id: "uVmBcfNISdRP", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateUserBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UserLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/poapprover/validaterole/browse
        [HttpPost("validaterole/browse")]
        [FwControllerMethod(Id: "4FCH5Db0a3Jb", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateRoleBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PoApproverRoleLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/poapprover/validatelocation/browse
        [HttpPost("validatelocation/browse")]
        [FwControllerMethod(Id: "25EcxFRp8VeU", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateLocationAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OfficeLocationLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/poapprover/validatedepartment/browse
        [HttpPost("validatedepartment/browse")]
        [FwControllerMethod(Id: "SMwT5lKbhXJv", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
    }
}
