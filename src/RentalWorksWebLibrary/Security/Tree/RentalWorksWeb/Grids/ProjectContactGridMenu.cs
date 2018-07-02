namespace FwStandard.Security.Tree.Grids
{
    public class ProjectContactGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ProjectContactGridMenu() : base("{F0D3B8C2-1CEC-41B6-81E4-D7B9C821684B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{837998F0-9752-4E45-BE91-43142AD1E011}", MODULEID);
            tree.AddNewMenuBarButton("{3A5EF6D7-550C-4B35-95C3-1A27AA7EFE0B}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{46280398-D801-4B6B-9FDB-FAC6CF94E908}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{912EDEE5-F50E-4E72-9000-95C2AAA07FF9}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}