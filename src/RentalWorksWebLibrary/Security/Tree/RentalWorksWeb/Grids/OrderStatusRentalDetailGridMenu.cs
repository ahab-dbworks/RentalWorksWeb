using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class OrderStatusRentalDetailGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderStatusRentalDetailGridMenu() : base("{5B497696-B956-453D-A2A0-755B84F8E83D}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{A6A1C1AB-3205-40E4-ACF1-95D9BEC89B2B}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}