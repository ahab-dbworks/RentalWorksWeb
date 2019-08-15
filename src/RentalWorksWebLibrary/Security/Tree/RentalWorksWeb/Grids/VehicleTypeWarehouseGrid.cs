using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class VehicleTypeWarehouseGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VehicleTypeWarehouseGrid() : base("{51707760-645D-452C-A545-37A4C861B139}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{D0FAC42A-3367-4993-A8B1-008EF6E2C327}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{3E00B3C2-7061-4621-A5BE-EA7E35659F79}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{302633B4-AA09-4F64-80A5-BF71ACB5A70A}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{1C57F411-5BC5-4126-8289-6E6084715411}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{F2C56529-7F4C-44DC-B2AE-77037A213C3C}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
