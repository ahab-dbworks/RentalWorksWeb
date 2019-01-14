using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class InvoiceReceiptGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InvoiceReceiptGrid() : base("{1C5F43F4-7428-4246-95B2-F45CE950CAFF}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{F83BDA34-06B3-41F4-9F2A-CA32CE390FEC}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}
