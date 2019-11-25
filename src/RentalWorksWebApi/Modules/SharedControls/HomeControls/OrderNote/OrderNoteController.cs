using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.HomeControls.OrderNote
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"8aq0E3nK2upt")]
    public class OrderNoteController : AppDataController
    {
        public OrderNoteController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderNoteLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordernote/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"F9EXpIkxfbsM", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"7maCDki6M8lb", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordernote 
        [HttpGet]
        [FwControllerMethod(Id:"tvfQ1umA6IZV", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<OrderNoteLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderNoteLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordernote/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"5TOXhl853VX0", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<OrderNoteLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderNoteLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordernote 
        [HttpPost]
        [FwControllerMethod(Id:"glgGtELdJMhO", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<OrderNoteLogic>> NewAsync([FromBody]OrderNoteLogic l)
        {
            return await DoNewAsync<OrderNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/ordernote/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "xbdVPzNdC23qM", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<OrderNoteLogic>> EditAsync([FromRoute] string id, [FromBody]OrderNoteLogic l)
        {
            return await DoEditAsync<OrderNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordernote/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"VdRXtiXDsn1Q", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<OrderNoteLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
