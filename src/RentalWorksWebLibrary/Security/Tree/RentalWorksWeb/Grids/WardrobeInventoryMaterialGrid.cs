using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class WardrobeInventoryMaterialGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WardrobeInventoryMaterialGrid() : base("{8BE5E66E-35B8-444F-9F9A-E03F4667F67A}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{86758005-0C8F-48F7-8B43-77445DC43B92}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{D0BDEF85-602D-40F5-8C91-71A91A89FF01}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{D21DECE6-6512-4653-86E0-737FCFFB26AB}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{8BF39CFA-F363-45F7-8E7C-5A639C6B37C1}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{952A7713-3D48-4036-AB41-B1BC09E6D7BD}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{6031547D-7745-41B7-AFEC-20BC81B2B3CB}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{CCFC518C-0E51-4124-AB0B-AB14A15697FC}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
