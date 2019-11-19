using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.ProjectSettings.ProjectDeposit
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"z9uSXy8A4AdoO")]
    public class ProjectDepositController : AppDataController
    {
        public ProjectDepositController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ProjectDepositLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectdeposit/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"e29zbhEzFWy75", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"HInRJD33MFpU4", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectdeposit 
        [HttpGet]
        [FwControllerMethod(Id:"siZU7a1VIIto8", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<ProjectDepositLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProjectDepositLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectdeposit/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"ljBEAlapGgRZe", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<ProjectDepositLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProjectDepositLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectdeposit 
        [HttpPost]
        [FwControllerMethod(Id:"9mEuS4V9UwWMI", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<ProjectDepositLogic>> NewAsync([FromBody]ProjectDepositLogic l)
        {
            return await DoNewAsync<ProjectDepositLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/projectdeposit/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "yRfIlg6zVGIfX", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<ProjectDepositLogic>> EditAsync([FromRoute] string id, [FromBody]ProjectDepositLogic l)
        {
            return await DoEditAsync<ProjectDepositLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/projectdeposit/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"lvlU4iGwXIiUD", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ProjectDepositLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
