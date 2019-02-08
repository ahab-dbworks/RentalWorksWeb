using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class WarehouseInventoryTypeGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WarehouseInventoryTypeGrid() : base("{D90C2659-F1FB-419D-89B6-738766DFCAD2}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{96D3B669-FD46-4E17-8334-5C74380EA70E}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{7FECE5CA-86A8-4F24-B0C4-7CC5B3F82E73}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{43427597-640C-4393-ABA3-3C5A0195B478}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{203076EE-64AB-4910-AA16-E2F24A667652}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{D234E26B-E3E1-42E4-B0A0-058D2C9A5B35}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
