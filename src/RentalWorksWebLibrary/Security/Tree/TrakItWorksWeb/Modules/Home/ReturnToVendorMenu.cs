using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakItWorksWeb.Modules.Settings
{
    public class ReturnToVendorMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ReturnToVendorMenu() : base("{79EAD1AF-3206-42F2-A62B-DA1C44092A7F}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{C6610E2E-CA71-430E-A5B2-378763B2A4C5}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{1AEEFF44-D674-4987-B8D1-5984BE6FCCAB}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}