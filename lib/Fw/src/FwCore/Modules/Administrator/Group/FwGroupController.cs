using FwCore.Controllers;
using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace FwCore.Modules.Administrator.Group
{
    [Route("api/v1/[controller]")]
    public abstract class FwGroupController : FwDataController
    {
        public FwGroupController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        protected async Task<ActionResult<FwAmSecurityTreeNode>> DoGetApplicationTree([FromRoute]string id)
        {
            FwAmGroupTree groupTree = await FwAppManager.Tree.GetGroupsTreeAsync(id, false);
            return new OkObjectResult(groupTree.RootNode);
        }
        //---------------------------------------------------------------------------------------------

    }
}