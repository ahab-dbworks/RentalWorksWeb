using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class SpaceGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SpaceGridMenu() : base("{BF54AEF8-BECB-4069-A1E3-3FEA27301AE8}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{493862AF-75E0-4760-9094-84AE114074B9}", MODULEID);
            tree.AddEditMenuBarButton("{27D7A35F-11BA-463A-B3E2-79CEF687A809}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}