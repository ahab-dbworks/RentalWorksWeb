using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class SpaceWarehouseRateGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SpaceWarehouseRateGridMenu() : base("{0F264871-A72C-48F7-9A6C-891208F52AD1}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{C5306005-348E-4558-B5FA-26B2B5F25E4A}", MODULEID);
            tree.AddEditMenuBarButton("{FE733A54-2C8F-4FC0-9A72-F0CF6584F084}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}