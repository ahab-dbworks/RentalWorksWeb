using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class InvoiceRevenueGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InvoiceRevenueGrid() : base("{8066A976-772F-4CCF-A227-EF4EE95CA137}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{61B7D6D3-0126-48A7-BA26-D05646C17970}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}
