using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class InventoryAvailabilityGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryAvailabilityGridMenu() : base("{8241ACB4-9346-43D6-8D3C-B6567FAA0270}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{5F781FBE-3CA4-45B6-860F-B58B602FF826}", MODULEID);
            tree.AddEditMenuBarButton("{2F0015D5-E2A8-4A8A-84B7-3CFFC5A73DB2}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}