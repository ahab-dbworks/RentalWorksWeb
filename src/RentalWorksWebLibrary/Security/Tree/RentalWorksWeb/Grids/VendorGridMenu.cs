using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class VendorGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VendorGridMenu() : base("{BA43D0E0-119D-495B-B066-8E5E738CFC4C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{F3848F08-D28E-4956-B45C-019A9FE180CA}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{C851EFB0-204B-406A-8AA2-046CCFC9ED86}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{8527A40A-361F-41D3-98D6-DA72F1EF9D7A}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{4242744D-2E8A-4173-BF40-055B50700EB7}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{F106812B-C943-49F7-833D-25535CB8F3C9}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{F86FA6CE-923B-418C-9893-4729360647D7}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{ED57FDBD-8E6A-4335-AE68-40540419235A}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
