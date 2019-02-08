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
            var nodeGridSubMenu = tree.AddSubMenu("{0664BB97-58BD-4C22-B2C9-F2E4FAC93A81}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{0C26727F-6D96-46FF-A8D0-811C9A070F6C}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{F6C0FCB0-591E-4B3E-9CFB-C8E0E39254D4}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}