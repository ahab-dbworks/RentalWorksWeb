namespace FwStandard.Security.Tree.Grids
{
    public class ContactNoteGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContactNoteGridMenu() : base("{A9CB5D4D-4AC0-46D4-A084-19039CF8C654}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{6CE9E3C6-6D1E-4B55-B1B3-7E7AD9B66815}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{25D1BD52-233B-48BD-BCD9-DEBABAFA9E88}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{6C59510B-8A21-4B2F-AD57-F5236B327CE1}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{1F0868FF-90D8-4A68-BA47-0B371C237CAD}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{D06EC9BE-2A02-4B5E-92D9-8530868A565A}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{1C79206D-7071-45B1-BC03-7D969A81022E}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{BA816B39-A08B-4B21-B32B-69484701BD9E}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}