namespace FwStandard.Security.Tree.Grids
{
    public class CompanyContactGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CompanyContactGridMenu() : base("{4172C587-7968-4664-A836-83A14A5B2B48}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{7DC07B83-B64D-4839-B6E4-16EA1C9BC8C2}", MODULEID);
            tree.AddNewMenuBarButton("{7D2AB3A6-FA80-4929-90A3-7C4442414628}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{E2AF52AA-0A5E-4763-B6DE-3A735BBBE1E3}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{97A86FC2-2CAD-495A-881C-DEF9DFF9FFF8}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}