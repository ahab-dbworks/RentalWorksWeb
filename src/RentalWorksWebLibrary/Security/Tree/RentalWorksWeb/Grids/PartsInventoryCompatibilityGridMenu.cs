using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class PartsInventoryCompatibilityGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PartsInventoryCompatibilityGridMenu() : base("{97DC0D58-2968-47F4-970A-0889AEFDC63B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{A3F11A4F-D68D-47F6-9996-8845661F8833}", MODULEID);
            tree.AddEditMenuBarButton("{67EAE20F-CA9D-4E73-93A5-0BD3FC654AD1}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{51C2B9A4-32EE-4CBD-8EBF-05683EE2585A}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{8DFB6054-2338-4D21-80B3-70A0A3DD292E}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}