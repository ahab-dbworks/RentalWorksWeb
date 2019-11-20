using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace FwCore.Controllers
{
    public class FwGroupSecurityTreeController : FwController
    {
        FwApplicationConfig appConfig;
        //------------------------------------------------------------------------------------
        public FwGroupSecurityTreeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig)
        {
            this.appConfig = appConfig.Value;
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<ActionResult<FwAmSecurityTreeNode>> DoGetAsync(string fileName, string downloadAsFileName)
        {
            FwAmGroupTree groupTree = await FwAppManager.Tree.GetGroupsTreeAsync(this.UserSession.GroupsId, false);
            return new OkObjectResult(groupTree.RootNode);
        }
        //------------------------------------------------------------------------------------
    }
}