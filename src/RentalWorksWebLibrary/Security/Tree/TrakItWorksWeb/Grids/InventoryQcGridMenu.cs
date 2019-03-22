using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class InventoryQcGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryQcGridMenu() : base("{8A0A4336-2124-4274-BF9F-ED2CE8CFFE54}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{61A8A8C9-4353-48EA-B579-AB1795CD8848}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{1074F97B-7752-4312-8644-B5F29193C1D9}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{D5A2DFCF-90AB-4E47-BB68-AFF529A0EA32}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{2CB43563-EC88-42CF-8B6B-D3D48CEB690C}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{BC15188B-783C-455B-A6A9-E5E75854B4D1}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
