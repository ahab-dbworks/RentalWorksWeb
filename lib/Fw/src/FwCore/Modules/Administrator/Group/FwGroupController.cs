using FwCore.Controllers;
using FwStandard.Models;
using FwStandard.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace FwCore.Modules.Administrator.Group
{
    [Route("api/v1/[controller]")]
    public class FwGroupController : FwDataController
    {
        public FwGroupController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        protected async Task<IActionResult> DoGetApplicationTree([FromRoute]string id)
        {
            FwSecurityTreeNode groupTree = await FwSecurityTree.Tree.GetGroupsTreeAsync(id, false);
            return new OkObjectResult(groupTree);
        }
        //---------------------------------------------------------------------------------------------

    }
}