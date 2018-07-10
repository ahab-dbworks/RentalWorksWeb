using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
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
        }
        //---------------------------------------------------------------------------------------------
    }
}