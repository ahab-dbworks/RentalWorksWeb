using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Home
{
    public class StagingCheckoutMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public StagingCheckoutMenu() : base("{C3B5EEC9-3654-4660-AD28-20DE8FF9044D}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{77A89D47-5E85-49AD-A8E3-450F80C8340A}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{4E8CFCEF-73C0-45F3-BB89-85436A0DE213}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{5054A52A-2A56-415F-B4FC-D771CF342DF1}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{4934CEC1-E0DC-4541-A7F5-E41D48321E6B}", nodeFormSubMenu.Id);
            tree.AddSubMenuItem("Cancel Staging / Check-Out", "{6E95996C-E104-4BBA-BE13-5FD73E4AAD04}", nodeFormOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}