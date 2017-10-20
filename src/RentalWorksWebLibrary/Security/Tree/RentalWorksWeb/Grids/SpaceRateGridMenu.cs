using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class SpaceRateGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SpaceRateGridMenu() : base("{F0A6AFE7-3A31-4D2D-BC37-702D785C3734}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{197326D6-ACB6-47A0-9616-BFC902BBD3B3}", MODULEID);
            tree.AddEditMenuBarButton("{A855A438-6C59-4357-B713-B1351AE251F8}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{FC1C25E2-8DC3-459D-8605-04FEA1BC877A}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{859C429A-A7E2-4B13-B3DE-22FE179C86CA}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}