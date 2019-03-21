using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class SalesInventoryWarehouseGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SalesInventoryWarehouseGridMenu() : base("{85ED5C98-37AF-4A68-B97B-68EE253A1FD4}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{ED29675E-22D6-40B3-ABCE-C4843BBC3EEA}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{4E3021CE-824E-4286-855A-0D21D43E2657}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{2D6A587E-F7CC-4E12-A4A8-2AA57E6895F0}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{2878925E-357E-40C8-BF5E-B9A54B7FE477}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{7546CAF7-1FC7-4CAB-B6E6-E03BC34BD11F}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}