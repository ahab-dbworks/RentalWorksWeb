using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class MarketSegmentJobGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public MarketSegmentJobGridMenu() : base("{6CB1FD8E-5E6E-45BC-B0E6-AC8E06A38990}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{0A014A97-9FC2-4ADA-A225-8CD2ADCC6A84}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{E575DFA5-7BFE-4954-B741-762793BA1B50}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{1724E81F-4B55-46F1-AB31-F0484D45D264}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{7832FEDE-F6F1-4544-BF78-5D50ECBAE5FE}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{FD6BFA89-4627-4AB5-879B-8720ACDFDAB6}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{A5A508A7-66B6-45CB-9B69-E3C092695592}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{B953B4A9-3717-4A66-A98C-75E6383FE218}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}