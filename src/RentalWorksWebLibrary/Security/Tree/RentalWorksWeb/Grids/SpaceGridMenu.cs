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
            tree.AddEditMenuBarButton("{441AFC51-AFDB-4772-85FF-BFE84134A048}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{3FECD487-0347-4C6A-A258-E0BCECA0E87F}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{2FCD3018-5B4A-41D5-B2FB-4CE864AAC7F4}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}