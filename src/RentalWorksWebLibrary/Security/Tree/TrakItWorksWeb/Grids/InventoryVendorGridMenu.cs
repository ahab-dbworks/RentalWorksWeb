using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class InventoryVendorGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryVendorGridMenu() : base("{EFE6D013-E84D-4EF8-A19F-571B5A1353FE}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{2F839323-424F-4DCE-B110-66991CD00810}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{B64908F8-70BF-4AAF-953A-C214E0EACF63}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{B6C3CB38-566C-4E5F-B114-43F56A68716A}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{5E8CA48D-D3A1-4C82-BC44-A96AD8075C8F}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{74FE4F38-1C28-4A75-A7CA-084BDCC4CEDF}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{02D5E17B-E94F-455F-A221-7F0D2E383647}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{32A74CC1-4A10-42A5-91BE-B061E42FE2C7}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
