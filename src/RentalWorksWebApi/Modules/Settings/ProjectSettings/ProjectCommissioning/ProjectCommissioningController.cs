using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.ProjectSettings.ProjectCommissioning
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"124H9oI67IKRx")]
    public class ProjectCommissioningController : AppDataController
    {
        public ProjectCommissioningController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ProjectCommissioningLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectcommissioning/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"9J1O4rJKcgUpm", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"fyicx5e4WILRf", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectcommissioning 
        [HttpGet]
        [FwControllerMethod(Id:"lba3Suyrz4GA4", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<ProjectCommissioningLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProjectCommissioningLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectcommissioning/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"Y1xQhZnRHCpcN", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<ProjectCommissioningLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProjectCommissioningLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectcommissioning 
        [HttpPost]
        [FwControllerMethod(Id:"YIvQ2tsPSAUv2", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<ProjectCommissioningLogic>> NewAsync([FromBody]ProjectCommissioningLogic l)
        {
            return await DoNewAsync<ProjectCommissioningLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/projectcommissioning/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "pHkHoDOjRfcdQ", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<ProjectCommissioningLogic>> EditAsync([FromRoute] string id, [FromBody]ProjectCommissioningLogic l)
        {
            return await DoEditAsync<ProjectCommissioningLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/projectcommissioning/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"kpB5az5SmLHPM", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ProjectCommissioningLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
