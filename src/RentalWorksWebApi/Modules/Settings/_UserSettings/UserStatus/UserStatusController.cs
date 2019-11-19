using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.UserSettings.UserStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"YjSbfCF9CEvjz")]
    public class UserStatusController : AppDataController
    {
        public UserStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(UserStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/userstatus/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"wfUvdn7BDqDaQ", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"EkWUqLFfdsktm", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/userstatus
        [HttpGet]
        [FwControllerMethod(Id:"A9jrHOlblgS9H", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<UserStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<UserStatusLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/userstatus/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"vSHrn1p2g66s5", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<UserStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<UserStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/userstatus
        [HttpPost]
        [FwControllerMethod(Id:"rsXRIdViEokGH", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<UserStatusLogic>> NewAsync([FromBody]UserStatusLogic l)
        {
            return await DoNewAsync<UserStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/userstatu/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "LE0zqR3ZV7Bd0", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<UserStatusLogic>> EditAsync([FromRoute] string id, [FromBody]UserStatusLogic l)
        {
            return await DoEditAsync<UserStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/userstatus/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"rD0jCFZI7LpwS", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<UserStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
