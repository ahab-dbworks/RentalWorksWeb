using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    class WarehouseQuikLocateApproverGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WarehouseQuikLocateApproverGrid() : base("{4B6EE2AA-8F4B-44FE-A08C-4D9DADD2B56A}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{EACC04DF-522D-403C-A85B-D9BE9222403A}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{C1174153-CC8F-4097-9362-235EFF21F10D}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{93C8D92B-223C-4F37-B6D5-9C5A4DF6E2E4}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{DC34566B-08CE-469F-B906-4D9782FF23BD}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{FD785F43-AC7B-4D1D-AAB8-21410460DDE0}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{4EB1C99E-C946-43BC-B959-0DC38F9BF67F}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{B56C54EC-1352-4500-AA42-2293B2BFB2E7}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
