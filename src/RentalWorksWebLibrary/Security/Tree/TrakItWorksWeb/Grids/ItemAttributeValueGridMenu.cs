using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class ItemAttributeValueGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ItemAttributeValueGridMenu() : base("{122AC4CB-831E-4F20-BC83-F12AD96094DE}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{FC14A3B8-03A4-4711-8D40-04A6A6192C21}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{27328C61-AC85-44C3-82F0-B092944AC7C4}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{155446C7-807A-4F16-BFB8-8BDF85E5CCF1}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{B7FE3E9E-3AA5-43B1-9116-847DBCAB9ED3}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{A2FE2832-2063-46A3-938D-DBD885A8E2E1}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{F1D5BA85-6B90-4032-9E6F-0221C65DBA50}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{48C6FA2D-0E42-41A0-9905-F13CF1E6E904}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
