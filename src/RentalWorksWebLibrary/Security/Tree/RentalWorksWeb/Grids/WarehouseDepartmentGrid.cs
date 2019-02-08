using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class WarehouseDepartmentGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WarehouseDepartmentGrid() : base("{CB4CE3A5-6DCC-497D-84D1-0B3FBAAEB19B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{F9404E78-955B-44E6-9448-36E3B33369DB}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{E215B51C-E2E7-4E9C-B6FA-932FDEBD81E1}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{B714AC16-7696-450F-A3C4-264BE10BBC3D}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{C736029B-AF7A-4A84-BE7D-082A4C9DF123}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{355E582D-C791-4355-95B9-31AE121CC505}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
