using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Administrator.User
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    public class UserController : AppDataController
    {
        public UserController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(UserLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(UserLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/user 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<UserLogic>(pageno, pagesize, sort, typeof(UserLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/user/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<UserLogic>(id, typeof(UserLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]UserLogic l)
        {
            return await DoPostAsync<UserLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/user/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(UserLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}