using FwCore.Modules.Administrator.Group;
using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Modules.Administrator.Group
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id: "0vP4rXxgGL1M")]
    public class GroupController : FwGroupController
    {
        public GroupController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(GroupLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/group/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"UkkHHbGOUAAc", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"w1KgIzBQ9Abm", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/group 
        [HttpGet]
        [FwControllerMethod(Id:"LcoN7EcCzk43", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<GroupLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GroupLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/group/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"mFGVa0Lnwe4c", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<GroupLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<GroupLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/group 
        [HttpPost]
        [FwControllerMethod(Id:"omZs9dySCrG7", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<GroupLogic>> NewAsync([FromBody]GroupLogic l)
        {
            return await DoNewAsync<GroupLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/group/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "kdzcuD1mWNHty", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<GroupLogic>> EditAsync([FromRoute] string id, [FromBody]GroupLogic l)
        {
            return await DoEditAsync<GroupLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/group/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"jxY9aeG4nl9e", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<GroupLogic>(id);
        }
        //------------------------------------------------------------------------------------ 

        // GET api/v1/group/applicationtree/A0000001 
        [HttpGet("applicationtree/{id}")]
        [FwControllerMethod(Id:"js0NttPoJa9B", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwAmSecurityTreeNode>> GetApplicationTree([FromRoute]string id)
        {
            return await DoGetApplicationTree(id);
        }
        //---------------------------------------------------------------------------------------------

    }
}
