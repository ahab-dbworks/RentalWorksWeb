using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class InventoryQcGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryQcGridMenu() : base("{C1EE89A8-2C6C-4709-AB0C-2BBC062160B5}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{AAAA720D-EE63-4B46-9354-5D2E429A1633}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{FA12DC7C-2E79-42F4-AE3A-D64B9968D356}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{BE6768BC-3337-4DE8-BD2D-A9BE7824E23B}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{AAD6964C-022C-4681-9B0D-8729B0BEF1B1}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{2DC900F2-1D81-4DAC-9666-776E3F764F6B}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}