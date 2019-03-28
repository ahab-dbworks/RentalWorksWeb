using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class OrderStatusRentalDetailGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderStatusRentalDetailGridMenu() : base("{62B225DE-33AA-4EC1-88DE-C945AB6ECB0F}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{402EFEEF-E5AD-4FE4-96A0-C87515751224}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{E53E403A-A385-4784-9B36-B478AC694877}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{5B66C07B-B85C-4FC1-9176-82CAF42A6A9B}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{4509459A-8C23-4A3D-8B04-0BF5CD08DE7D}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
