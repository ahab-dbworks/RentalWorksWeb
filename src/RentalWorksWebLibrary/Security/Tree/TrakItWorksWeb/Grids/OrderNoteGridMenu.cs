using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    class OrderNoteGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderNoteGrid() : base("{929AF93E-F07D-4E78-8FDD-1F0FEC90D9A4}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{912FC904-FEE2-406A-BB13-AF400F293E58}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{9D208B6C-B04B-4513-9071-2DA9AFFF0952}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{2E87A3DB-ECC3-4974-B428-01E013D59165}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{B5D8D3F1-DA80-419D-B103-33C38F4F8A3C}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{F8F8A147-D872-4F42-BABE-D1B171FCDBBE}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{7865C616-015E-463A-BDDB-F89264EF988C}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{4DCE029F-46E0-451A-86BF-EE770AC42CF0}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
