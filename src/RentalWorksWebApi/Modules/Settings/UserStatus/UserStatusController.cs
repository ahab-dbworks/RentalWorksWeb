using FwStandard.SqlServer;
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
    public class UserStatusController : AppDataController
    {
        public UserStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(UserStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/userstatus/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(UserStatusLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/userstatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<UserStatusLogic>(pageno, pagesize, sort, typeof(UserStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/userstatus/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<UserStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<UserStatusLogic>(id, typeof(UserStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/userstatus
        [HttpPost]
        public async Task<ActionResult<UserStatusLogic>> PostAsync([FromBody]UserStatusLogic l)
        {
            return await DoPostAsync<UserStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/userstatus/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(UserStatusLogic));
        }
        //------------------------------------------------------------------------------------
    }
}