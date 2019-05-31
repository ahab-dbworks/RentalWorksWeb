using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    class WarehouseDepartmentUserGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WarehouseDepartmentUserGrid() : base("{9DD3FB80-2287-484D-BD1D-38BCEE1E838E}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{75278E46-4D9E-42EB-83AA-0BBD4AFBD087}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{C168787A-AA25-4DFB-963E-0E0C765C7D6A}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{796CC06A-0814-4DF7-BCC4-6CCA3C82EC3B}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{F65F8635-23B9-479D-8FC7-1EFBC0027971}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{876020D7-8B09-479B-84FE-1A809CC05058}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
