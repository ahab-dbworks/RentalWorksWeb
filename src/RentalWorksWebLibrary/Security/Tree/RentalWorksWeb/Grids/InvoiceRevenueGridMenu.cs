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
            var nodeGridSubMenu = tree.AddSubMenu("{1CE7C398-2FAA-440A-989E-AB05F5FBC630}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{759E5981-B237-483C-BA23-0B1D3962605A}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{B62AF4D0-5864-446F-BB5A-C1FC78FE44DE}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
