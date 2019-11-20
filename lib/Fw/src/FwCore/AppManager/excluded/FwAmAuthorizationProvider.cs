using FwStandard.AppManager;
using FwStandard.Security;
using System.Threading.Tasks;

namespace FwCore.AppManager
{
    public class FwAmAuthorizationProvider
    {
        public static async Task<bool> IsAuthorizedAsync(string groupsid, string securityid, bool requireVisible, bool requireEditable)
        {
            bool isAuthorized = false;
            var groupTree = await FwAppManager.Tree.GetGroupsTreeAsync(groupsid, false);
            if (groupTree.RootNode != null)
            {
                var node = groupTree.RootNode.FindById(securityid);
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
