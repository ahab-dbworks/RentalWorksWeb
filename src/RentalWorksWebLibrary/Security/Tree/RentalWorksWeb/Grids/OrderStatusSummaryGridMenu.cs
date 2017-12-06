using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class OrderStatusSummaryGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderStatusSummaryGridMenu() : base("{959E3D3C-B83D-4ACC-997D-A5508DE0A542}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{90DD5FC2-D29D-4340-BBCB-2355DB420844}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}