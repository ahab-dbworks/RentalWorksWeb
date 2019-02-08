using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class CustomerNoteGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CustomerNoteGridMenu() : base("{50EB024E-6D9A-440A-8161-458A2E89EFB8}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{4C2DE149-AAE0-449E-B044-D8A570DC974F}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{415D8F6B-48FC-4EAF-B472-16AD4B51E37D}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{469108E5-BDBD-4CB3-A2F0-D7E7AE98D223}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{068A5D50-227B-4A87-B1DA-EBE6389C86B8}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{69E2267D-4CEF-4B97-B6F4-01AC1E4EE803}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{10EC35E9-BC23-452B-89DD-1617B7F2B8D4}",   nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{C62F8041-1398-4CA1-87BB-8833F5FED485}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}