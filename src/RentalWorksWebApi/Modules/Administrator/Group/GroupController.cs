using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using FwStandard.Security;

namespace WebApi.Modules.Administrator.Group
{
    [Route("api/v1/[controller]")]
    public class GroupController : AppDataController
    {
        public GroupController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/group/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(GroupLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/group 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GroupLogic>(pageno, pagesize, sort, typeof(GroupLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/group/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<GroupLogic>(id, typeof(GroupLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/group 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]GroupLogic l)
        {
            return await DoPostAsync<GroupLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/group/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(GroupLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/group/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 

        // GET api/v1/group/applicationtree/A0000001 
        [HttpGet("applicationtree/{id}")]
        public async Task<IActionResult> GetApplicationTree([FromRoute]string id)
        {
            //return await DoGetAsync<GroupLogic>(id, typeof(GroupLogic));
       
            //const string METHOD_NAME = "FwGroup.getApplicationTree";
            FwSecurityTreeNode groupTree;
            //string groupsid;

            //FwValidate.TestPropertyDefined(METHOD_NAME, request, "groupsid");
            //groupsid = FwCryptography.AjaxDecrypt(request.groupsid);
            groupTree = Task.Run<FwSecurityTreeNode>(async () => await FwSecurityTree.Tree.GetGroupsTreeAsync(id, false)).Result;

            //response.applicationtree = groupTree;
            return new OkObjectResult(groupTree);
        }
        //---------------------------------------------------------------------------------------------

    }
}