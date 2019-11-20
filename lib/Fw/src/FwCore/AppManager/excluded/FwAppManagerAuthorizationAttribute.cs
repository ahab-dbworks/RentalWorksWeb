using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace FwCore.AppManager
{
    //[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    //class FwAppManagerAuthorizationAttribute : AuthorizeAttribute, IAuthorizationFilter, IActionFilter
    //{
    //    public FwAppManagerAuthorizationAttribute(AuthorizationPolicyBuilder policy) : base(policy)
    //    {
            
    //    }

    //    public void OnActionExecuted(ActionExecutedContext context)
    //    {
    //        var controller = context.Controller;
    //        var action = context.ActionDescriptor;
    //    }

    //    public void OnActionExecuting(ActionExecutingContext context)
    //    {
    //        var controller = context.Controller;
    //        var action = context.ActionDescriptor;
    //    }

    //    public void OnAuthorization(AuthorizationFilterContext context)
    //    {
    //        var controller = context.Controller;
    //        var action = context.ActionDescriptor;
    //    }

        
    //}
}
