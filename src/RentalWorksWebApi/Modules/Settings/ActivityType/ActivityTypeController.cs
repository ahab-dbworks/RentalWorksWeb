using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Settings.ActivityType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id: "dZaqY68fhRSXm")]
    public class ActivityTypeController : AppDataController
    {
        public ActivityTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ActivityTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/activitytype/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "dZeRdrjOf5Ri7", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/activitytype/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "dzpHbRh4GkLdM", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/activitytype/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "SimkgFK38JhXq", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            legend.Add("User Activity", RwGlobals.USER_DEFINED_ACTIVITY_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/activitytype 
        [HttpGet]
        [FwControllerMethod(Id: "dZYTnIyHRp3fJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<ActivityTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ActivityTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/activitytype/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "E0qiVxnrgPx1F", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<ActivityTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ActivityTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/activitytype 
        [HttpPost]
        [FwControllerMethod(Id: "e0rtMGOLRVyVL", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<ActivityTypeLogic>> NewAsync([FromBody]ActivityTypeLogic l)
        {
            return await DoNewAsync<ActivityTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/activitytype/A0000001 
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "e0RXiCFBIHPyr", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<ActivityTypeLogic>> EditAsync([FromRoute] string id, [FromBody]ActivityTypeLogic l)
        {
            return await DoEditAsync<ActivityTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/activitytype/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "E10YxfpJDacoF", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ActivityTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
