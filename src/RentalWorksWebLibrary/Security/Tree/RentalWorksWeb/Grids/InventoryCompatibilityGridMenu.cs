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
        }
        //---------------------------------------------------------------------------------------------
    }
}