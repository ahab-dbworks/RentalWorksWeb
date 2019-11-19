using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Administrator.CustomField
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id:"cZHPJQyBxolS")]
    public class CustomFieldController : AppDataController
    {
        public CustomFieldController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CustomFieldLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customfield/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"MVqKL0mhgdEq", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"2AQiFN6G0C6q", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/customfield 
        [HttpGet]
        [FwControllerMethod(Id:"48aNAVK56csQ", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<IEnumerable<CustomFieldLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomFieldLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/customfield/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"fJIMAF8iRQP8", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<CustomFieldLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CustomFieldLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customfield 
        [HttpPost]
        [FwControllerMethod(Id:"Ev6izRtzAgqI", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<CustomFieldLogic>> NewAsync([FromBody]CustomFieldLogic l)
        {
            return await DoNewAsync<CustomFieldLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/customfield/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "ftJom0TG4NK9R", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<CustomFieldLogic>> EditAsync([FromRoute] string id, [FromBody]CustomFieldLogic l)
        {
            return await DoEditAsync<CustomFieldLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/customfield/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"6PHk5J2N5N1t", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CustomFieldLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
