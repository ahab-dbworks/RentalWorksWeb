using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using System.Threading.Tasks;
using FwStandard.Security;
using FwCore.Modules.Administrator.Group;

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
        [FwControllerMethod(Id:"UkkHHbGOUAAc")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"w1KgIzBQ9Abm")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/group 
        [HttpGet]
        [FwControllerMethod(Id:"LcoN7EcCzk43")]
        public async Task<ActionResult<IEnumerable<GroupLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GroupLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/group/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"mFGVa0Lnwe4c")]
        public async Task<ActionResult<GroupLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<GroupLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/group 
        [HttpPost]
        [FwControllerMethod(Id:"omZs9dySCrG7")]
        public async Task<ActionResult<GroupLogic>> PostAsync([FromBody]GroupLogic l)
        {
            return await DoPostAsync<GroupLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/group/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"jxY9aeG4nl9e")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<GroupLogic>(id);
        }
        //------------------------------------------------------------------------------------ 

        // GET api/v1/group/applicationtree/A0000001 
        [HttpGet("applicationtree/{id}")]
        [FwControllerMethod(Id:"js0NttPoJa9B")]
        public async Task<ActionResult<FwSecurityTreeNode>> GetApplicationTree([FromRoute]string id)
        {
            return await DoGetApplicationTree(id);
        }
        //---------------------------------------------------------------------------------------------

    }
}
