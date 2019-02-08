using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class OrderTypeContactTitleGridGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderTypeContactTitleGridGridMenu() : base("{E104C48C-2579-4674-9BD1-41069AC6968B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{3416D13B-87C2-4793-952E-ACB521940FAE}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{B0BF7A9F-5C41-40C5-8166-75BF4F31C3D9}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{C89C6D47-1E63-4803-8CD0-166182112227}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{748BC595-F094-4C00-B9E4-09CF8268AC1B}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{8994E705-2D70-4694-91C9-92043C5EAADD}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{EB9C7DDC-7B25-490B-A1BB-9B6E7B7685F9}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{83DBE1B1-2FA2-4F48-AE19-FF2F7A5E943C}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
