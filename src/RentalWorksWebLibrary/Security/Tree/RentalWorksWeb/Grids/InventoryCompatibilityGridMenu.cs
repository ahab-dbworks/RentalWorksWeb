using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class InventoryCompatibilityGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryCompatibilityGridMenu() : base("{7416DAAE-2875-408B-AEEF-78481378C4C4}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{49820EE4-1F5A-4946-8DE8-41386DB20E6A}", MODULEID);
            tree.AddEditMenuBarButton("{FE7CA2EC-C8C0-4E07-A487-E00F3FAC4DBB}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{D5F0BE5C-5454-4B3F-A985-27C0333EFB0A}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{9C0C6EA4-4544-43AA-BE3E-8E821149B052}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}