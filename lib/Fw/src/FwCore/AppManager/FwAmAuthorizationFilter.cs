using FwCore.Api;
using FwStandard.AppManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
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
                    var hasTokenTypeClaim = context.HttpContext.User.Claims.Any(c => c.Type == AuthenticationClaimsTypes.TokenType && !string.IsNullOrEmpty(c.Value));
                    if (hasTokenTypeClaim)
                    {
                        var tokenType = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == AuthenticationClaimsTypes.TokenType).Value;
                        if (tokenType == TokenTypes.Service)
                        {
                            var hasControllerIdFilterClaim = context.HttpContext.User.Claims.Any(c => c.Type == AuthenticationClaimsTypes.ControllerIdFilter && !string.IsNullOrEmpty(c.Value));
                            if (hasControllerIdFilterClaim)
                            {
                                var controllerAttributes = (FwControllerAttribute[])controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes(typeof(FwControllerAttribute), false);
                                if (controllerAttributes.Length == 0)
                                {
                                    // reject the request, because the [FwController] attribute is missing from the Controller
                                    context.Result = new ForbidResult();
                                    return Task.CompletedTask;
                                }
                                var controllerAttribute = controllerAttributes[0];

                                var controllerIdFilter = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == AuthenticationClaimsTypes.ControllerIdFilter).Value;
                                List<string> controllerIds = new List<string>(controllerIdFilter.Split(",", StringSplitOptions.RemoveEmptyEntries));
                                if (!controllerIds.Contains(controllerAttribute.Id))
                                {
                                    context.Result = new ForbidResult();
                                }
                                return Task.CompletedTask;
                            }

                            var hasMethodIdFilterClaim = context.HttpContext.User.Claims.Any(c => c.Type == AuthenticationClaimsTypes.MethodIdFilter && !string.IsNullOrEmpty(c.Value));
                            if (hasMethodIdFilterClaim)
                            {
                                var methodAttributes = (FwControllerMethodAttribute[])controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(FwControllerMethodAttribute), false);
                                if (methodAttributes.Length == 0)
                                {
                                    // reject the request, because the [FwControllerMethod] attribute is missing from the Method
                                    context.Result = new ForbidResult();
                                    return Task.CompletedTask;
                                }
                                var methodAttribute = methodAttributes[0];

                                var methodIdFilter = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == AuthenticationClaimsTypes.MethodIdFilter).Value;
                                List<string> methodIds = new List<string>(methodIdFilter.Split(",", StringSplitOptions.RemoveEmptyEntries));
                                {
                                    context.Result = new ForbidResult();
                                }
                                return Task.CompletedTask;
                            }
                        }
                        else if (tokenType == "INTEGRATION")
                        {
                            return Task.CompletedTask;
                        }
                        //else if (tokenType == "INTEGRATION")
                        //{
                        //    return Task.CompletedTask;
                        //}
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
