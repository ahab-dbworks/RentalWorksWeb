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
        }
        //---------------------------------------------------------------------------------------------
    }
}