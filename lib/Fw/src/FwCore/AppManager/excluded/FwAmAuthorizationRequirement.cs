using Microsoft.AspNetCore.Authorization;

namespace FwCore.AppManager
{
    public class FwAmAuthorizationRequirement : IAuthorizationRequirement
    {
        public string SecurityId { get; set; }
        public bool RequireVisible { get; set; }
        public bool RequireEditable { get; set; }
        public FwAmAuthorizationRequirement(string id, bool requireVisible, bool requireEditable)
        {
            this.SecurityId = id;
            this.RequireVisible = requireVisible;
            this.RequireEditable = requireEditable;
        }
    }
}
