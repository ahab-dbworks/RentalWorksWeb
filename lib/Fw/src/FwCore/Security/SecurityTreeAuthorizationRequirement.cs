using FwCore.Security;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FwCore.Security
{
    public class SecurityTreeAuthorizationRequirement : IAuthorizationRequirement
    {
        public string SecurityId { get; set;  }
        public bool RequireVisible { get; set;  }
        public bool RequireEditable { get; set;  }
        public SecurityTreeAuthorizationRequirement(string securityid, bool requireVisible, bool requireEditable)
        {
            SecurityId = securityid;
            RequireVisible = requireVisible;
            RequireEditable = requireEditable;
        }
    }


    public class SecurityTreeAuthorizationHandler : AuthorizationHandler<SecurityTreeAuthorizationRequirement>, IAuthorizationRequirement
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, SecurityTreeAuthorizationRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "http://www.dbworks.com/claims/groupsid"))
            {
                string groupsid = context.User.Claims.First<Claim>(c => c.Type == "http://www.dbworks.com/claims/groupsid").Value;
                bool isAuthorized = await SecurityTreeAuthorizationProvider.IsAuthorizedAsync(groupsid, requirement.SecurityId, requirement.RequireVisible, requirement.RequireEditable);
                if (isAuthorized)
                {
                    context.Succeed(requirement);
                }
            }
        }
    }
}
