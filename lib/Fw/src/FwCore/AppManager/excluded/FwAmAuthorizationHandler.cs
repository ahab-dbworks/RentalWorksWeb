using FwStandard.AppManager;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FwCore.AppManager
{
    public class FwAmAuthorizationHandler : AuthorizationHandler<FwAmAuthorizationRequirement>, IAuthorizationRequirement
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, FwAmAuthorizationRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "http://www.dbworks.com/claims/groupsid"))
            {
                string groupsid = context.User.Claims.First<Claim>(c => c.Type == "http://www.dbworks.com/claims/groupsid").Value;
                bool isAuthorized = false;
                FwAmGroupTree groupTree = await FwAppManager.Tree.GetGroupsTreeAsync(groupsid, false);
                if (groupTree != null)
                {
                    FwAmSecurityTreeNode node = groupTree.RootNode.FindById(requirement.SecurityId);
                    if (node != null)
                    {
                        bool visibleRequirementSatisfied  = (requirement.RequireVisible && node.Properties.ContainsKey("visible") && node.Properties["visible"] == "T") || !requirement.RequireVisible;
                        bool editableRequirementSatisfied = (requirement.RequireEditable && node.Properties.ContainsKey("editable") && node.Properties["editable"] == "T") || !requirement.RequireEditable;
                        if (node != null && visibleRequirementSatisfied && editableRequirementSatisfied)
                        {
                            isAuthorized = await Task.FromResult<bool>(true);
                        }
                    }
                }
                if (isAuthorized)
                {
                    context.Succeed(requirement);
                }
            }
        }
    }
}
