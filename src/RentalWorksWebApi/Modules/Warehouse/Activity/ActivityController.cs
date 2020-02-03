using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
using WebApi.Modules.Settings.ActivityStatus;
using WebApi.Modules.Administrator.User;
using WebApi.Modules.Settings.ActivityType;

namespace WebApi.Modules.Warehouse.Activity
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "warehouse-v1")]
    [FwController(Id: "hb52dbhX1mNLZ")]
    public class ActivityController : AppDataController
    {
        public ActivityController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ActivityLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/activity/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "hbxB4lkqGW1Cc", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/activity/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "HBzMVbnsD1xOU", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/activity 
        [HttpGet]
        [FwControllerMethod(Id: "HCC9LsTYnihhc", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<ActivityLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ActivityLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/activity/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "hCRmllkN7i1iF", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<ActivityLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ActivityLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/activity 
        [HttpPost]
        [FwControllerMethod(Id: "hCrqWIhmu5hOq", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<ActivityLogic>> NewAsync([FromBody]ActivityLogic l)
        {
            return await DoNewAsync<ActivityLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/activity/A0000001 
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "HdjMfJo1jBt08", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<ActivityLogic>> EditAsync([FromRoute] string id, [FromBody]ActivityLogic l)
        {
            return await DoEditAsync<ActivityLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/activity/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "HDKy8swBuurzB", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ActivityLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/activity/validateactivitystatus/browse 
        [HttpPost("validateactivitystatus/browse")]
        [FwControllerMethod(Id: "Rhcp0WRMpW24", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateActivityStatusBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ActivityStatusLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/activity/validateuser/browse 
        [HttpPost("validateuser/browse")]
        [FwControllerMethod(Id: "2ZC5cQRsi0fw", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateUserBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UserLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/activity/validateactivitytype/browse 
        [HttpPost("validateactivitytype/browse")]
        [FwControllerMethod(Id: "dFPeo2R5tybj", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateActivityTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ActivityTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
