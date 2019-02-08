using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class SpaceWarehouseRateGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SpaceWarehouseRateGridMenu() : base("{0F264871-A72C-48F7-9A6C-891208F52AD1}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{C5306005-348E-4558-B5FA-26B2B5F25E4A}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{EB4EFE70-36C1-4016-9CA9-0A5B9ACCF372}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{05D51952-010F-44C2-B6CC-4FAE92704435}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{FA15FBC0-4DE3-4813-B9D6-AA0085B5A037}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{FE733A54-2C8F-4FC0-9A72-F0CF6584F084}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}