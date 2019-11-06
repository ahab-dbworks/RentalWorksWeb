using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.PersonalEvent
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"35was7r004gg")]
    public class PersonalEventController : AppDataController
    {
        public PersonalEventController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PersonalEventLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/personalevent/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"9zdXEEhoo6Dd")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/personalevent/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"nXXURNnhZZ08")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/personalevent 
        [HttpGet]
        [FwControllerMethod(Id:"tUz5BVaMa7Cs")]
        public async Task<ActionResult<IEnumerable<PersonalEventLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PersonalEventLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/personalevent/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"cMQp1zMXJqMv")]
        public async Task<ActionResult<PersonalEventLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PersonalEventLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/personalevent 
        [HttpPost]
        [FwControllerMethod(Id:"ktguNM2bYJOo")]
        public async Task<ActionResult<PersonalEventLogic>> PostAsync([FromBody]PersonalEventLogic l)
        {
            return await DoPostAsync<PersonalEventLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/personalevent/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"hkBVS0fJnzDP")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PersonalEventLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
