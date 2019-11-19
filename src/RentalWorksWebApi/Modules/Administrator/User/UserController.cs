using FwStandard.AppManager;
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
    [FwController(Id:"r1fKvn1KaFd0u")]
    public class UserController : AppDataController
    {
        public UserController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(UserLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"funlZdaTqF6fg", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"0yn5pKJgieEge", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/user 
        [HttpGet]
        [FwControllerMethod(Id:"J1fKwMfRirqIj", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<UserLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<UserLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/user/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"a39bfoeajNGtD", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<UserLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<UserLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user 
        [HttpPost]
        [FwControllerMethod(Id:"hwJfXH8OWTYWf", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<UserLogic>> NewAsync([FromBody]UserLogic l)
        {
            return await DoNewAsync<UserLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/user/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "b1hVJqkdyadvj", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<UserLogic>> EditAsync([FromRoute] string id, [FromBody]UserLogic l)
        {
            return await DoEditAsync<UserLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/user/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"4QiCY7sXmbJUS", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<UserLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
