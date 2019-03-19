using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class ReceiveFromVendorMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ReceiveFromVendorMenu() : base("{EC4052D5-664E-4C34-8802-78E086920628}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse

            // Form
            var nodeForm = tree.AddForm("{1B02C18E-E0E6-4530-BEA3-A828A7636892}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{2F73E2E4-AA72-4F6F-A90E-976DA202CC6F}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}