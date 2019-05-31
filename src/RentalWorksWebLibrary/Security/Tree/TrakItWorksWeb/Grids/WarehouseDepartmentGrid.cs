using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    class WarehouseDepartmentGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WarehouseDepartmentGrid() : base("{C1A66C89-20EC-42CC-84FA-787F5BD7093F}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{65C9639B-87A6-4638-A953-86F33579202D}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{6B154AB9-286D-4E25-849E-3BE401086C18}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{99DACE87-F721-4BA8-9822-597D25BC3D21}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{C6261451-AF09-4E78-BCEF-61F302896929}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{54725755-62A9-4A72-8816-107FFD1DBEA8}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
