using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class InventoryContainerItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryContainerItemGridMenu() : base("{494F7DD0-0D32-4FE0-B84A-BC7CD71CE9EC}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{9ABD5355-3FD4-430B-9E43-78678A7FB7D9}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{DD1F4765-FA03-44E5-83BE-264D1E8457C8}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{D05339A3-E68C-4B68-9E7D-4E927A677580}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{26ACD418-C572-4BEC-9312-87735112FABD}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{15F089E9-0D51-400E-B7BB-135C5B983FF0}",   nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{999FD148-9F94-444D-815A-3A7194C19682}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{4A3D8689-86DF-4F77-8730-63343EAD47C2}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}