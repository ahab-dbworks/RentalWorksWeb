using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.UserStatus
{
    [Route("api/v1/[controller]")]
    public class UserStatusController : AppDataController
    {
        public UserStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(UserStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/userstatus/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(UserStatusLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/userstatus
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<UserStatusLogic>(pageno, pagesize, sort, typeof(UserStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/userstatus/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<UserStatusLogic>(id, typeof(UserStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/userstatus
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]UserStatusLogic l)
        {
            return await DoPostAsync<UserStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/userstatus/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(UserStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/userstatus/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}