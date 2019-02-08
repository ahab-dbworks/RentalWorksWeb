using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class SpaceRateGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SpaceRateGridMenu() : base("{F0A6AFE7-3A31-4D2D-BC37-702D785C3734}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{197326D6-ACB6-47A0-9616-BFC902BBD3B3}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{CD5F6E6B-AD1C-4236-8D87-F2EADD6CBD6C}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{C43A9B7A-ADE8-4BA3-B84E-F6876B4CAE0F}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{90BF28FF-8F0C-497B-831C-5DCA8811D02D}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{A855A438-6C59-4357-B713-B1351AE251F8}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{FC1C25E2-8DC3-459D-8605-04FEA1BC877A}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{859C429A-A7E2-4B13-B3DE-22FE179C86CA}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}