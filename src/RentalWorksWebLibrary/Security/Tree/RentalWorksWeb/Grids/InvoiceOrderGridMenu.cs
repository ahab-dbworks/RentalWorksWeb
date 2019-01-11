using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class InvoiceOrderGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InvoiceOrderGrid() : base("{D4B2DBB4-FDB8-461E-A3BE-EE81F43A61C6}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{6748F828-5120-4E52-86AD-9499EF58351D}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}
