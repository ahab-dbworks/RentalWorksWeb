using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    class POReceiveBarCodeGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public POReceiveBarCodeGrid() : base("{6781A0C0-14FB-4B3B-970F-EF9FC812E835}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{09B3625E-C655-40D4-A4B4-E4A33318F4C4}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{738560B6-64E4-4EA3-A4BA-9618F3EA7C33}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{F411AB48-3AA8-44FC-A7C3-13AFDE1DA112}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{7BA263E2-F0C9-4BC3-9326-947B0C69B23F}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{95C9BFC4-49E0-4B1E-B49A-D0898423A1EC}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{99D83ED0-271B-416A-9EE2-7001100477BB}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

