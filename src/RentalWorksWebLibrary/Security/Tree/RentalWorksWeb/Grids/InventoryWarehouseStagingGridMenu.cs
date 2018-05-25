using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class InventoryWarehouseStagingGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryWarehouseStagingGridMenu() : base("{3D9F7C07-4B47-4E4C-B573-331D694B979E}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{636CD191-B0D4-4033-8817-64B828F0BEA2}", MODULEID);
            tree.AddEditMenuBarButton("{27F85CF1-4173-4FCF-810F-42C926C35E26}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}