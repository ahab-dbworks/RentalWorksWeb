using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class ReturnToVendorMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ReturnToVendorMenu() : base("{D54EAA01-A710-4F78-A1EE-5FC9EE9150D8}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{532D0CB7-792E-47EC-AC65-EF35E49E2A09}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{817FB552-F2CA-4798-9F76-5AD7C489F8F0}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{5A087379-46E2-419B-A5C3-886D23C7F22D}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{067CD80C-2B3A-46A5-B7C4-216B1B482D9E}", nodeFormSubMenu.Id);
            tree.AddSubMenuItem("Cancel Return To Vendor", "{C072441D-1FE3-4D2E-A015-DBE871CEC0FD}", nodeFormOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}