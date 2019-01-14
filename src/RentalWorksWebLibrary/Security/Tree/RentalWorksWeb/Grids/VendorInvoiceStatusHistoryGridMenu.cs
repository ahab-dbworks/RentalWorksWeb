using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class VendorInvoiceStatusHistoryGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VendorInvoiceStatusHistoryGrid() : base("{4CCDDE3F-57CD-43B4-88F0-F8B59AF104F9}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{297FF109-D5D5-4DCB-A834-30BEA8FDF9CF}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}
