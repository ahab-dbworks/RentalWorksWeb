using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FwCore.Api
{
    public class FwAppManagerAuthorizationRequirement : IAuthorizationRequirement
    {
        public FwAppManagerAuthorizationRequirement()
        {
            
        }
    }


    public class FwAppManagerAuthorizationHandler : AuthorizationHandler<FwAppManagerAuthorizationRequirement>, IAuthorizationRequirement
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, FwAppManagerAuthorizationRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "http://www.dbworks.com/claims/groupsid"))
            {
                string groupsid = context.User.Claims.First<Claim>(c => c.Type == "http://www.dbworks.com/claims/groupsid").Value;
                bool isAuthorized = await FwAppManagerAuthorizationProvider.IsAuthorizedAsync(groupsid, requirement.SecurityId, requirement.RequireVisible, requirement.RequireEditable);
                if (isAuthorized)
                {
                    context.Succeed(requirement);
                }
            }
        }
    }

    public class FwAppManagerAuthorizationProvider
    {
        public static async Task<bool> IsAuthorizedAsync(string groupsid, string securityid, bool requireVisible, bool requireEditable)
        {
            bool isAuthorized = false;
            //FwSecurityTreeNode groupTree = await FwSecurityTree.Tree.GetGroupsTreeAsync(groupsid, false);
            //if (groupTree != null)
            //{
            //    FwSecurityTreeNode node = groupTree.FindById(securityid);
            //    if (node != null)
            //    {
            //        bool visibleRequirementSatisfied  = (requireVisible && node.Properties.ContainsKey("visible") && node.Properties["visible"] == "T") || !requireVisible;
            //        bool editableRequirementSatisfied = (requireEditable && node.Properties.ContainsKey("editable") && node.Properties["editable"] == "T") || !requireEditable;
            //        if (node != null && visibleRequirementSatisfied && editableRequirementSatisfied)
            //        {
            //            isAuthorized = await Task.FromResult<bool>(true);
            //        }
            //    }
            //}
            await Task.CompletedTask;
            return isAuthorized;
        }
    }
}
