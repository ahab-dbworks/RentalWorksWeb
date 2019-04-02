using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Utilities
{
    public class ActivityCalendarMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ActivityCalendarMenu() : base("{897BCF55-6CE7-412C-82CB-557B045F8C0A}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{4E34D403-B499-4449-98D2-4EEDC7E36F81}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{2B737ECA-39DA-485D-A3E4-2A67B8D4533F}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}