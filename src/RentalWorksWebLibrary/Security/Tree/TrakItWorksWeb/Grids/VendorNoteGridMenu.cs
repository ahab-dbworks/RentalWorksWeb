using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    class VendorNoteGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VendorNoteGridMenu() : base("{87681463-5C45-4A84-ABEB-FE010E31BC06}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{74C67AE0-F6D9-4842-A800-95D4D2158511}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{8C35471A-7B04-4973-A37A-51D242A93DDA}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{FAB9947F-9159-4EFA-9C6F-4D96B77380FB}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{C9D696E7-A3E1-4065-8EED-3167824FEB04}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{B446F349-6E9A-40EA-82BF-71A53641E118}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{571B4C8B-D362-4E6F-AB6D-193F74C6A78A}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{5E3A3EDB-DFAF-4221-8FA7-9C4A31FE1ADB}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
