using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class SubPurchaseOrderItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SubPurchaseOrderItemGridMenu() : base("{27A93B3D-4E30-4854-88C0-292783E778B3}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{1CCC68F7-743E-4B39-AC54-FD11F6CA60CF}", MODULEID);
            tree.AddEditMenuBarButton("{D94CE001-4223-463B-9D0C-1423B9B1AA22}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}