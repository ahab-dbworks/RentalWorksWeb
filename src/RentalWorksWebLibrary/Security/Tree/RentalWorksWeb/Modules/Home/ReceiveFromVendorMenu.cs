using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class ReceiveFromVendorMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ReceiveFromVendorMenu() : base("{00539824-6489-4377-A291-EBFE26325FAD}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse

            // Form
            var nodeForm = tree.AddForm("{1562F6FA-6EBC-401E-A3F1-E23EAB024AAD}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{9BF31149-D0C3-451B-8483-631D013C5E0A}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}