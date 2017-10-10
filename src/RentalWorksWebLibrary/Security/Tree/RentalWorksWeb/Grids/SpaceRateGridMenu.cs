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
            tree.AddEditMenuBarButton("{5F6055E7-A631-404C-8FBA-80D79FA754A4}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}