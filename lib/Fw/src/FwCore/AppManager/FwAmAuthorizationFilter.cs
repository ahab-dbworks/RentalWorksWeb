using FwCore.Api;
using FwStandard.AppManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FwCore.AppManager
{
    class FwAmAuthorizationFilter : IAsyncAuthorizationFilter
    {
        public FwAmAuthorizationFilter() : base()
        {

        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var relativeUrl = context.HttpContext.Request.Path.Value;
            if (context.ActionDescriptor is ControllerActionDescriptor)
            {
                var controllerActionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
                var controllerMethodAttributes = (FwControllerMethodAttribute[])controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(FwControllerMethodAttribute), false);
                if (controllerMethodAttributes.Length == 0)
                {
                    // reject the request, because the [ControllerMethod] attribute is missing from the Controller method
                    context.Result = new ForbidResult();
                    return Task.CompletedTask;
                }
                else
                {
                    var controllerMethodAttribute = controllerMethodAttributes[0];
                    if (controllerMethodAttribute.AllowAnonymous)
                    {
                        return Task.CompletedTask;
                    }
                    var hasVersionClaim = context.HttpContext.User.Claims.Any(c => c.Type == AuthenticationClaimsTypes.Version && !string.IsNullOrEmpty(c.Value));
                    if (!hasVersionClaim)
                    {
                        context.Result = new UnauthorizedResult();
                        return Task.CompletedTask;
                    }
                    var version = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == AuthenticationClaimsTypes.Version).Value;
                    if (version != FwProgram.ServerVersion)
                    {
                        context.Result = new UnauthorizedResult();
                        return Task.CompletedTask;
                    }
                    var hasWebUsersIdClaim = context.HttpContext.User.Claims.Any(c => c.Type == AuthenticationClaimsTypes.WebUsersId && !string.IsNullOrEmpty(c.Value));
                    if (!hasWebUsersIdClaim)
                    {
                        context.Result = new ForbidResult();
                        return Task.CompletedTask;
                    }
                    var hasGroupsIdClaim = context.HttpContext.User.Claims.Any(c => c.Type == AuthenticationClaimsTypes.GroupsId && !string.IsNullOrEmpty(c.Value));
                    if (!hasGroupsIdClaim)
                    {
                        context.Result = new ForbidResult();
                        return Task.CompletedTask;
                    }
                    var hasValidGroupsDateStampClaim = context.HttpContext.User.Claims.Any(c => c.Type == AuthenticationClaimsTypes.GroupsDateStamp && !string.IsNullOrEmpty(c.Value));
                    if (!hasValidGroupsDateStampClaim)
                    {
                        context.Result = new ForbidResult();
                        return Task.CompletedTask;
                    }

                    // Validate Product Edition
                    var editions = controllerMethodAttribute.Editions;
                    if (editions == null)
                    {
                        editions = FwAppManager.Tree.GetDefaultEditions();
                    }
                    if (!FwAppManager.Tree.HasProductEdition(editions, FwAppManager.CurrentProduct, FwAppManager.CurrentProductEdition))
                    {
                        // reject the request because the Controller Action is not supported in this product edition
                        context.Result = new ForbidResult();
                        return Task.CompletedTask;
                    }

                    // Validate that the User's secrity group exists and is not expired
                    var groupsid = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == AuthenticationClaimsTypes.GroupsId).Value;
                    var groupsdatestamp = Convert.ToDateTime(context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == AuthenticationClaimsTypes.GroupsDateStamp).Value);
                    var groupTree = FwAppManager.Tree.GetGroupsTreeAsync(groupsid, true).Result;
                    if (groupTree == null)
                    {
                        // reject the request because the group does not exist
                        context.Result = new UnauthorizedResult();
                        return Task.CompletedTask;
                    }
                    if (groupsdatestamp != groupTree.DateStamp)
                    {
                        // reject the request because someone has modified the user's group since they last logged in
                        context.Result = new UnauthorizedResult();
                        return Task.CompletedTask;
                    }
                    if (controllerMethodAttribute.ValidateSecurityGroup)
                    {
                        var groupTreeNode = groupTree.RootNode.FindById(controllerMethodAttribute.Id);
                        if (groupTreeNode == null || !groupTreeNode.Properties.ContainsKey("visible") || groupTreeNode.Properties["visible"] == "F")
                        {
                            context.Result = new ForbidResult();
                            return Task.CompletedTask;
                        }
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
