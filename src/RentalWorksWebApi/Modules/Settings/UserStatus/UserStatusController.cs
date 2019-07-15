using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.UserStatus
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
        [FwControllerMethod(Id:"wfUvdn7BDqDaQ")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"EkWUqLFfdsktm")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/userstatus
        [HttpGet]
        [FwControllerMethod(Id:"A9jrHOlblgS9H")]
        public async Task<ActionResult<IEnumerable<UserStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<UserStatusLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/userstatus/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"vSHrn1p2g66s5")]
        public async Task<ActionResult<UserStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<UserStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/userstatus
        [HttpPost]
        [FwControllerMethod(Id:"rsXRIdViEokGH")]
        public async Task<ActionResult<UserStatusLogic>> PostAsync([FromBody]UserStatusLogic l)
        {
            return await DoPostAsync<UserStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/userstatus/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"rD0jCFZI7LpwS")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<UserStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
