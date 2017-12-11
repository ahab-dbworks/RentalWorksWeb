using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class DiscountItemMiscGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DiscountItemMiscGridMenu() : base("{5974DBEF-1D45-4B11-BA85-CB05B725F54C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{3E071890-EECF-46A2-81CB-ED59247F9906}", MODULEID);
            tree.AddEditMenuBarButton("{94290905-032D-44BC-9B25-07A32A409C34}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{557F366F-11CF-4D8E-AEEE-D4F3514BFD0E}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{F933C11A-F4AB-48B8-8CD9-6BD977B28539}", nodeGridMenuBar.Id);

        }
        //---------------------------------------------------------------------------------------------
    }
}