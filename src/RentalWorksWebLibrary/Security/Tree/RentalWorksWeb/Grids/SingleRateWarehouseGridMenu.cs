using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class SingleRateWarehouseGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SingleRateWarehouseGridMenu() : base("{0E4E4B5D-5905-4BD5-AC57-2DE047EFEB5B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{D6A118BF-0F92-4983-888E-BFAAF867C873}", MODULEID);
            tree.AddEditMenuBarButton("{9F2BFE87-63B6-40F2-AFB3-428260F0665A}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}