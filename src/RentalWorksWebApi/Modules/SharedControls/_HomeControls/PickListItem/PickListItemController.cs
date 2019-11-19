using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.HomeControls.PickListItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"fa0NLEHNkNU0")]
    public class PickListItemController : AppDataController
    {
        public PickListItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PickListItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistitem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"ejfRES4aamWo", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"HWCLQBBjiv4f", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/picklistitem 
        [HttpGet]
        [FwControllerMethod(Id:"cYxg8XgpAPWI", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<PickListItemLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PickListItemLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/picklistitem/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"1kLiOK8S7Uvu", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<PickListItemLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PickListItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistitem 
        [HttpPost]
        [FwControllerMethod(Id:"11TRe2zbBfgX", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<PickListItemLogic>> NewAsync([FromBody]PickListItemLogic l)
        {
            return await DoNewAsync<PickListItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/picklistitem/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "X4CipSl7ufdDs", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<PickListItemLogic>> EditAsync([FromRoute] string id, [FromBody]PickListItemLogic l)
        {
            return await DoEditAsync<PickListItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/picklistitem/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"KsjnO2LSTkTG", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PickListItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
