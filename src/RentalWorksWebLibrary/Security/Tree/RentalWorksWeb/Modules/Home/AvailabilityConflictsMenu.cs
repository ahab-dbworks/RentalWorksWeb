using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Home
{
    public class AvailabilityConflictsMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public AvailabilityConflictsMenu() : base("{DF2859D1-3834-42DA-A367-85B168850ED9}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeForm = tree.AddForm("{D70E4815-591D-4904-8B31-0B18D5203D22}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{26A2F4D5-059E-4D4F-B8C7-B5B6B16FB8CA}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}