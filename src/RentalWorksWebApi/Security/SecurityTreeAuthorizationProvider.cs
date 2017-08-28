using FwStandard.Security;
using RentalWorksWebLibrary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Security
{
    public class SecurityTreeAuthorizationProvider
    {
        public static async Task<bool> IsAuthorizedAsync(string groupsid, string securityid, bool requireVisible, bool requireEditable)
        {
            bool isAuthorized = false;
            FwSecurityTreeNode groupTree = await SecurityTree.Tree.GetGroupsTreeAsync(groupsid, false);
            if (groupTree != null)
            {
                FwSecurityTreeNode node = groupTree.FindById(securityid);
                if (node != null)
                {
                    bool visibleRequirementSatisfied  = (requireVisible && node.Properties.ContainsKey("visible") && node.Properties["visible"] == "T") || !requireVisible;
                    bool editableRequirementSatisfied = (requireEditable && node.Properties.ContainsKey("editable") && node.Properties["editable"] == "T") || !requireEditable;
                    if (node != null && visibleRequirementSatisfied && editableRequirementSatisfied)
                    {
                        isAuthorized = await Task.FromResult<bool>(true);
                    }
                }
            }
            return isAuthorized;
        }
    }
}
