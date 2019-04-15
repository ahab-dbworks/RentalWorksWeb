using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Home
{
    public class ContainerStatusMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContainerStatusMenu() : base("{0CD07ACF-D9A4-42A3-A288-162398683F8A}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{1572B08A-D02B-4C09-BFA6-85FC52D17B20}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{81B73005-18F0-44D7-8F82-59E40242DB96}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}