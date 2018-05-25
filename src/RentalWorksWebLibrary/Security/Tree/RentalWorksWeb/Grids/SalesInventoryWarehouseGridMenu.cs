using FwStandard.Security;

namespace SalesWorksWebLibrary.Security.Tree.SalesWorksWeb.Grids
{
    public class SalesInventoryWarehouseGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SalesInventoryWarehouseGridMenu() : base("{85ED5C98-37AF-4A68-B97B-68EE253A1FD4}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{ED29675E-22D6-40B3-ABCE-C4843BBC3EEA}", MODULEID);
            tree.AddEditMenuBarButton("{7546CAF7-1FC7-4CAB-B6E6-E03BC34BD11F}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}