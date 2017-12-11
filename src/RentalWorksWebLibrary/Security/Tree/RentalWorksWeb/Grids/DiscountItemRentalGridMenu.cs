using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class DiscountItemRentalGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DiscountItemRentalGridMenu() : base("{FF124304-4048-4A1F-A6DA-2F79343BCE87}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{10066CF6-EA59-477B-8F78-6A524FEC6386}", MODULEID);
            tree.AddEditMenuBarButton("{CBCF7A5F-A39C-4371-BA49-664861B673B0}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{A02623E7-8D25-4E66-AF4F-575DC93C58A9}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{688E9E00-7F30-4718-94DB-426FC6E3D0E6}", nodeGridMenuBar.Id);

        }
        //---------------------------------------------------------------------------------------------
    }
}