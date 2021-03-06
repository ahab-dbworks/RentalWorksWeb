using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.OrderTypeNote
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"DZwS6DaO7Ed8")]
    public class OrderTypeNoteController : AppDataController
    {
        public OrderTypeNoteController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderTypeNoteLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertypenote/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"k7aJYIHPMRbS", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"oOzTanu5XU0k", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertypenote 
        [HttpGet]
        [FwControllerMethod(Id:"mm5M36UzcUta", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<OrderTypeNoteLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderTypeNoteLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertypenote/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"4pnkjjAhy0MZ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<OrderTypeNoteLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderTypeNoteLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertypenote 
        [HttpPost]
        [FwControllerMethod(Id:"Iz2jswBTyjfV", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<OrderTypeNoteLogic>> NewAsync([FromBody]OrderTypeNoteLogic l)
        {
            return await DoNewAsync<OrderTypeNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/ordertypenote/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "LqGrssJUxGlm7", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<OrderTypeNoteLogic>> EditAsync([FromRoute] string id, [FromBody]OrderTypeNoteLogic l)
        {
            return await DoEditAsync<OrderTypeNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordertypenote/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"Ars9P9bCh9yg", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<OrderTypeNoteLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
