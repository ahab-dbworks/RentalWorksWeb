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
            // Form
            var nodeForm = tree.AddForm("{1562F6FA-6EBC-401E-A3F1-E23EAB024AAD}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{9BF31149-D0C3-451B-8483-631D013C5E0A}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{CB3B95C7-CBCA-4D9D-AB0F-EB05911F17A3}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{9466724B-C8FA-49B2-B76D-FF02C682666A}", nodeFormSubMenu.Id);
            tree.AddSubMenuItem("Cancel Receive From Vendor", "{A3BA715F-9249-4504-B076-1E9195F35372}", nodeFormOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}