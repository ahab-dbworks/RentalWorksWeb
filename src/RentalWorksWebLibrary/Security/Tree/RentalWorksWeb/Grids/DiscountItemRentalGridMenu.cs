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
            var nodeGridSubMenu = tree.AddSubMenu("{24CDAD07-6181-4AE5-B0C4-D9905B675AC6}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{C86044E5-A380-430E-B405-CD498B804E74}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{36B8B3AB-CAD0-4590-A063-62CBCD77E837}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{CBCF7A5F-A39C-4371-BA49-664861B673B0}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{A02623E7-8D25-4E66-AF4F-575DC93C58A9}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{688E9E00-7F30-4718-94DB-426FC6E3D0E6}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}