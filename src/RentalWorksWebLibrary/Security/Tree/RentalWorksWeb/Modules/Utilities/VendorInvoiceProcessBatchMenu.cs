using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class VendorInvoiceProcessBatchMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VendorInvoiceProcessBatchMenu() : base("{4FA8A060-F2DF-4E59-8F9D-4A6A62A0D240}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{79F55BE6-253A-4638-A58E-BAEB123E7EC9}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{BE323A2D-8CEC-43BD-8994-108C4E5DA50D}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}