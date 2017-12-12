using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class OrderStatusSalesDetailGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderStatusSalesDetailGridMenu() : base("{220300EC-40A7-4374-8247-BE6BFC5CDF14}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{3536BAAE-54BA-4CAC-94CC-847AB4651E15}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}