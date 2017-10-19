using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class DiscountItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DiscountItemGridMenu() : base("{2EB32722-33D0-43C4-B799-ECD81EDF9C99}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{FB0FE8C4-EB7F-48A5-A035-D9E92AC55F3C}", MODULEID);
                tree.AddEditMenuBarButton("{8AC0C78D-7988-4832-A05D-A4011A98A193}",   nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}