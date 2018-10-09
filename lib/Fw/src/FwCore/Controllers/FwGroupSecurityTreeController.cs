using FwStandard.Models;
using FwStandard.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        protected virtual async Task<ActionResult<FwSecurityTreeNode>> DoGetAsync(string fileName, string downloadAsFileName)
        {
            FwSecurityTreeNode groupTree = await FwSecurityTree.Tree.GetGroupsTreeAsync(this.UserSession.GroupsId, false);
            return new OkObjectResult(groupTree);
        }
        //------------------------------------------------------------------------------------
    }
}