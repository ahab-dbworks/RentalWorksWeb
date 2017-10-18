using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class InventoryConsignmentGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryConsignmentGridMenu() : base("{0D22AF5B-CF50-41EA-A8CC-D039C402E4CC}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{E8009EA8-54A9-4A60-B119-47AD41BA40E9}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}