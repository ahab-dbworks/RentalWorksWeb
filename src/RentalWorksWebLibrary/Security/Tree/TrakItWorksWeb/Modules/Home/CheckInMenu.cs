using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakItWorksWeb.Modules.Settings
{
    public class CheckInMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CheckInMenu() : base("{3D1EB9C4-95E2-440C-A3EF-10927C4BDC65}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{25901EBC-4224-44EF-8327-25C545D945B7}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{7B4AB2EB-37DC-4D18-94C2-442C1D4121DA}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}