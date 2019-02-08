using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class WarehouseDepartmentUserGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WarehouseDepartmentUserGrid() : base("{4B3FB84E-CC4D-4EAE-917A-1291B733AC89}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{37DDDD3B-1287-462E-A9B0-18EF6B5A60AE}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{8193D33E-9CE3-4DE7-82A1-D063CFC7B4E6}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{020B7644-B8DD-4450-835B-97D2569EFE22}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{179A41F5-06FC-4E55-B868-FCAB7E901E31}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{3DA5A867-A5EB-44DF-B635-F17F1A1E2808}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
