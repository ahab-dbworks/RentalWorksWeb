using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class InvoiceStatusHistoryGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InvoiceStatusHistoryGrid() : base("{08E2713B-9B57-4B1F-8859-E7B10E116EAA}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{A8CC4584-74E2-4231-ADEC-11688A16DA8B}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}
