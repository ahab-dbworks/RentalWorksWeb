using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.OrderSettings.OrderSetNo
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"OoepsrkqPYRP")]
    public class OrderSetNoController : AppDataController
    {
        public OrderSetNoController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderSetNoLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordersetno/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"S21ps5UYCTgd", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"2nWIZHuWwxdS", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordersetno 
        [HttpGet]
        [FwControllerMethod(Id:"PVe7DYvQXsjh", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<OrderSetNoLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderSetNoLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordersetno/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"3cS7eEZ92XOG", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<OrderSetNoLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderSetNoLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordersetno 
        [HttpPost]
        [FwControllerMethod(Id:"LlN0BR5vxZg3", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<OrderSetNoLogic>> NewAsync([FromBody]OrderSetNoLogic l)
        {
            return await DoNewAsync<OrderSetNoLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/ordersetno/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "9XHxfuJNV98jS", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<OrderSetNoLogic>> EditAsync([FromRoute] string id, [FromBody]OrderSetNoLogic l)
        {
            return await DoEditAsync<OrderSetNoLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordersetno/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"booSUfWreDwC", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<OrderSetNoLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
