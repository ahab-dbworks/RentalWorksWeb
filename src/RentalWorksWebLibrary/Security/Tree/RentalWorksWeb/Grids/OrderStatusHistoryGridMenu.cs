using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class OrderStatusHistoryGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderStatusHistoryGridMenu() : base("{D5B97814-9FD7-4821-9553-28D276F67797}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{8C067DE7-27CC-4B2B-9AE0-F772DEB5AF6D}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}