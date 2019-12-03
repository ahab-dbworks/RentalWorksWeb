using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.Modules.Administrator.AlertWebUsers;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.Administrator.User;
namespace WebApi.Modules.AdministratorControls.AlertWebUsers
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id: "REgcmntq4LWE")]
    public class AlertWebUsersController : AppDataController
    {
        public AlertWebUsersController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(AlertWebUsersLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/alertwebusers/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "W8tc28TbRfbq", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/alertwebusers/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "UyjhSZeRz0kiD", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/alertwebusers 
        [HttpGet]
        [FwControllerMethod(Id: "BLt1TByqGEDu", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<AlertWebUsersLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<AlertWebUsersLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/alertwebusers/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "6pbS1g4dEEHi", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<AlertWebUsersLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<AlertWebUsersLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/alertwebusers 
        [HttpPost]
        [FwControllerMethod(Id: "dPxB8IbKkXnJx", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<AlertWebUsersLogic>> NewAsync([FromBody]AlertWebUsersLogic l)
        {
            return await DoNewAsync<AlertWebUsersLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/alertwebusers/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "DUleDE4IWs1L8", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<AlertWebUsersLogic>> EditAsync([FromRoute] string id, [FromBody]AlertWebUsersLogic l)
        {
            return await DoEditAsync<AlertWebUsersLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/alertwebusers/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "B866tX0CwraC", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<AlertWebUsersLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/alertwebusers/validateuser/browse
        [HttpPost("validateuser/browse")]
        [FwControllerMethod(Id: "4RkyReVIXu77", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateUserBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UserLogic>(browseRequest);
        }
    }
}
