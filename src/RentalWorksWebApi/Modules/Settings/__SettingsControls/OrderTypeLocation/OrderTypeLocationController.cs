using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.OrderTypeLocation
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"acguZNBoT1XC")]
    public class OrderTypeLocationController : AppDataController
    {
        public OrderTypeLocationController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderTypeLocationLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertypelocation/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"w2G9A3OXHJXB", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"11U1X0aOAkPX", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertypelocation 
        [HttpGet]
        [FwControllerMethod(Id:"McjfvmVDBgY6", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<OrderTypeLocationLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderTypeLocationLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertypelocation/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"1Cfk7qV6R9NJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<OrderTypeLocationLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderTypeLocationLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertypelocation 
        [HttpPost]
        [FwControllerMethod(Id:"ADARVuVZLCPo", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<OrderTypeLocationLogic>> NewAsync([FromBody]OrderTypeLocationLogic l)
        {
            return await DoNewAsync<OrderTypeLocationLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/ordertypelocation/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "4PAT9fGmDJTHg", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<OrderTypeLocationLogic>> EditAsync([FromRoute] string id, [FromBody]OrderTypeLocationLogic l)
        {
            return await DoEditAsync<OrderTypeLocationLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordertypelocation/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"qD6v2cKyOtiv", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<OrderTypeLocationLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
