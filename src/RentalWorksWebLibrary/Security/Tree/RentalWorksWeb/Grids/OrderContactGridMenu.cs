namespace FwStandard.Security.Tree.Grids
{
    public class OrderContactGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderContactGridMenu() : base("{33321573-EB0C-43BE-9C95-739A879FC81B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{36603D54-317D-4501-A2AD-ACC2C93AD34E}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{82C2CAE4-19BC-4403-90BB-1B70390F6EB7}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{69BA0BA7-3E2D-49CA-BFF9-1E9DFEE139CF}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{65E8D275-72CD-4A7A-8C36-DE68D7B74BCD}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{4AFFB32C-3A01-42AB-876D-210D4D081DBF}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{931A1631-6AD7-40A8-A802-0FBCE43BA887}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{15A88CF3-8B10-47CD-A590-E8BB154591A4}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}