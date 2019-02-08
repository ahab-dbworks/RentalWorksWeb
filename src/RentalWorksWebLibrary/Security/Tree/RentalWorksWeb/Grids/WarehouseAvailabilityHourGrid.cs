using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class WarehouseAvailabilityHourGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WarehouseAvailabilityHourGrid() : base("{DF40BE8D-BAAA-45E8-A6AE-78057281C1EC}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{32EF6941-FA20-44BF-8B5C-68B4D9A6205B}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{432831B5-1D7B-441F-95E9-BCA12DCC8082}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{4850474C-AA77-4450-9DC0-E973F0C2B774}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{2733D78E-8119-4187-BBD4-562FFC95020C}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{5AEF3187-D378-4239-8A8E-F2C3B2527EEC}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{42757E96-92DA-4B1F-AFBA-F62ADAC703F3}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{F7FC0995-1B2C-4F2C-AE36-FE5D69F8A548}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
