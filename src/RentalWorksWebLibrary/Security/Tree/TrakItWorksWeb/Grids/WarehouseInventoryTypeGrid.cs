using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    class WarehouseInventoryTypeGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WarehouseInventoryTypeGrid() : base("{09705490-1824-4BBE-987E-40BB1508213C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{E8DAB9E2-5D26-4C97-B574-2319779E7CBD}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{B7A135E6-F128-48AA-AC0F-81526CEA093E}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{46A707C5-21B7-4CDD-90B6-D560C6F468B4}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{6F25E7B9-30F0-450E-A60D-59F3076248EF}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{551C8AC3-8339-4CFD-91A8-E2307909A334}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
