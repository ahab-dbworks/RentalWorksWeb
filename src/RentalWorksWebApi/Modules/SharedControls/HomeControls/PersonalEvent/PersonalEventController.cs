using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Modules.Settings.ContactSettings.ContactEvent;
namespace WebApi.Modules.HomeControls.PersonalEvent
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
        [FwControllerMethod(Id:"9zdXEEhoo6Dd", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/personalevent/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"nXXURNnhZZ08", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/personalevent 
        [HttpGet]
        [FwControllerMethod(Id:"tUz5BVaMa7Cs", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<PersonalEventLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PersonalEventLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/personalevent/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"cMQp1zMXJqMv", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<PersonalEventLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PersonalEventLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/personalevent 
        [HttpPost]
        [FwControllerMethod(Id:"ktguNM2bYJOo", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<PersonalEventLogic>> NewAsync([FromBody]PersonalEventLogic l)
        {
            return await DoNewAsync<PersonalEventLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/personalevent/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "n8aIrtAVo77iQ", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<PersonalEventLogic>> EditAsync([FromRoute] string id, [FromBody]PersonalEventLogic l)
        {
            return await DoEditAsync<PersonalEventLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/personalevent/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"hkBVS0fJnzDP", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PersonalEventLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/personalevent/validatecontactevent/browse
        [HttpPost("validatecontactevent/browse")]
        [FwControllerMethod(Id: "41uQJPcnywPN", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateContactEventBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ContactEventLogic>(browseRequest);
        }
    }
}
