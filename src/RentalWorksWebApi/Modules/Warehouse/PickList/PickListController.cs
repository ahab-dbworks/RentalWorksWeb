using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Warehouse.PickList
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"bggVQOivrIgi")]
    public class PickListController : AppDataController
    {
        public PickListController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PickListLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklist/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"0Fe6HLUi5BwB", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"qnIOsIxD0g9k", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/picklist 
        [HttpGet]
        [FwControllerMethod(Id:"SirTwaedErp0", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<PickListLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PickListLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/picklist/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"GGu5kB35Emav", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<PickListLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PickListLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklist 
        [HttpPost]
        [FwControllerMethod(Id:"hTnBK5zVzZUu", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<PickListLogic>> NewAsync([FromBody]PickListLogic l)
        {
            return await DoNewAsync<PickListLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/picklist/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "wcoGRoVubL7yi", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<PickListLogic>> EditAsync([FromRoute] string id, [FromBody]PickListLogic l)
        {
            return await DoEditAsync<PickListLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/picklist/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"2zX9FJ9f8TX5", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PickListLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
