using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class VendorInvoicePaymentGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VendorInvoicePaymentGrid() : base("{8B63442E-DE46-47BD-B995-342B2A49E77E}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{652B1D29-014B-4F88-B24F-00B133A98A63}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}
