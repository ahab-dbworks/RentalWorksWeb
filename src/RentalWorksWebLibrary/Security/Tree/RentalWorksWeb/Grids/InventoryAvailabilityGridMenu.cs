using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class InventoryAvailabilityGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryAvailabilityGridMenu() : base("{8241ACB4-9346-43D6-8D3C-B6567FAA0270}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{5F781FBE-3CA4-45B6-860F-B58B602FF826}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}