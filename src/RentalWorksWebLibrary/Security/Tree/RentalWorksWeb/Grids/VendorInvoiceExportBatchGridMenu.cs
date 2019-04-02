using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class VendorInvoiceExportBatchGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VendorInvoiceExportBatchGridMenu() : base("{DA44AE35-7F98-4740-AE76-366C421A13BD}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{80FDBE72-19D5-4753-8E8C-0FE1CD4AD303}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{08CFD509-FA7B-4714-8DCF-00C7F31AA67B}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}