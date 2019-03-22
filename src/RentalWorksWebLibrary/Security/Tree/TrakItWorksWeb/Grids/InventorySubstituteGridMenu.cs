using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class InventorySubstituteGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventorySubstituteGridMenu() : base("{AAF0CDF9-30DC-4A7A-883C-5363F694D843}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{3F51B3B7-F362-4397-A71A-E3DDB0B0C624}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{1D822B2E-8F9A-4720-AF53-58C9F35AEB1A}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{3528D114-9339-4021-93FD-D87DD465765A}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{2EA802F9-A614-4D70-A77A-90879B48F85C}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{52B2C410-20BC-4D32-A0C0-B86750C1D2DD}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{CF4DE766-ABC1-4863-A5D6-D3EA4FA66831}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{775A28EA-8447-486B-A97C-6E4F09D19996}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
