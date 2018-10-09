using FwStandard.SqlServer;
using System.Collections.Generic;
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
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/user 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<UserLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/user/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<UserLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<UserLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user 
        [HttpPost]
        public async Task<ActionResult<UserLogic>> PostAsync([FromBody]UserLogic l)
        {
            return await DoPostAsync<UserLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/user/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}