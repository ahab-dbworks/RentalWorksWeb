namespace FwStandard.Security.Tree.Grids
{
    public class ContactNoteMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContactNoteMenu() : base("{A9CB5D4D-4AC0-46D4-A084-19039CF8C654}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{6CE9E3C6-6D1E-4B55-B1B3-7E7AD9B66815}", MODULEID);
                tree.AddNewMenuBarButton("{D06EC9BE-2A02-4B5E-92D9-8530868A565A}",    nodeGridMenuBar.Id);
                tree.AddEditMenuBarButton("{1C79206D-7071-45B1-BC03-7D969A81022E}",   nodeGridMenuBar.Id);
                tree.AddDeleteMenuBarButton("{BA816B39-A08B-4B21-B32B-69484701BD9E}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}