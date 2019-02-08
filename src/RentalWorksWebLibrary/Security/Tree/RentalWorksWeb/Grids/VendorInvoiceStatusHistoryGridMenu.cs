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
            var nodeGridSubMenu = tree.AddSubMenu("{D1B5DA01-1F7F-49AF-9695-E1CFCE25B508}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{A2C50681-950E-4FFC-A6DC-8DC9B547A9A3}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{8407E545-0C42-484F-9E30-4B960BDB0221}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
