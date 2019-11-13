using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class InventoryPurchaseItemGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryPurchaseItemGrid() : base("{5B55AF96-8267-4CE1-A4FB-4B050CC6E218}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{E9D1504F-DFEE-4EDA-B175-4515AFC9B5E2}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{0FB9FCFE-1C48-44CD-83F4-BD3D8EA6CFD9}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{57240E6C-58B7-43FC-82FD-AD7F7E4A1F0B}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{49487DC5-6B14-4BF8-9030-1B8C734B851D}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{B6333B20-DBCE-4D15-98EB-988652F92A6D}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{5B783D2A-5A87-4812-B3DF-1143C4A8AEF5}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
